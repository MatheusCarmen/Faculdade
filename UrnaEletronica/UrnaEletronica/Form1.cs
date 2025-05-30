using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UrnaEletronica
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Candidato> _dicPrefeito;
        private Dictionary<string, Candidato> _dicVereador;
        private Dictionary<string, int> votosVereador = new Dictionary<string, int>();
        private Dictionary<string, int> votosPrefeito = new Dictionary<string, int>();



        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
            _dicPrefeito = new Dictionary<string, Candidato>();
            _dicVereador = new Dictionary<string, Candidato>();

            // Prefeitos
            _dicPrefeito.Add("51", new Candidato() { Id = 51, Nome = "Clark Kent", Partido = "Cidadania", Foto = Properties.Resources.ClarkKent });
            _dicPrefeito.Add("52", new Candidato() { Id = 52, Nome = "Natasha Romanoff", Partido = "Trabalhista", Foto = Properties.Resources.NatashaRomanoff });

            // Vereadores
            _dicVereador.Add("11", new Candidato() { Id = 11, Nome = "Hermione Granger", Partido = "Cidadania", Foto = Properties.Resources.HermioneGranger });
            _dicVereador.Add("12", new Candidato() { Id = 12, Nome = "Bruce Williams", Partido = "Trabalhista", Foto = Properties.Resources.BruceWilliams });

        }


        private void btn1_Click(object sender, EventArgs e)
        {
            RegistrarDigito("1");
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            RegistrarDigito("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            RegistrarDigito("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            RegistrarDigito("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            RegistrarDigito("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            RegistrarDigito("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            RegistrarDigito("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            RegistrarDigito("8");
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            RegistrarDigito("9");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            RegistrarDigito("0");
        }
        private enum EtapaVotacao { Vereador, Prefeito, Fim }
        private EtapaVotacao etapaAtual;

        private void AtualizarPainel()
        {
            panelVereador.Visible = false;
            panelPrefeito.Visible = false;
            pnFim.Visible = false;

            if (etapaAtual == EtapaVotacao.Vereador)
            {
                panelVereador.Visible = true;
                panelVereador.BringToFront();
            }
            else if (etapaAtual == EtapaVotacao.Prefeito)
            {
                panelPrefeito.Visible = true;
                panelPrefeito.BringToFront();
            }
            else if (etapaAtual == EtapaVotacao.Fim)
            {
                pnFim.Visible = true;
                pnFim.BringToFront();
                MostrarResultados();
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            etapaAtual = EtapaVotacao.Vereador;
            InicializarCandidatos();
            AtualizarPainel();
        }

        private void RegistrarDigito(string digito)
        {
            if (etapaAtual == EtapaVotacao.Vereador)
            {
                if (string.IsNullOrEmpty(txtVereador1.Text))
                    txtVereador1.Text = digito;
                else
                {
                    txtVereador2.Text = digito;
                    PreencherVereador(txtVereador1.Text, txtVereador2.Text);
                }
            }
            else if (etapaAtual == EtapaVotacao.Prefeito)
            {
                if (string.IsNullOrEmpty(txtPrefeito1.Text))
                    txtPrefeito1.Text = digito;
                else
                {
                    txtPrefeito2.Text = digito;
                    PreencherPrefeito(txtPrefeito1.Text, txtPrefeito2.Text);
                }
            }

            SoundPlayer s = new SoundPlayer(Properties.Resources.clique);
            s.Play();
        }

        private void LimparVereador()
        {
            txtVereador1.Text = "";
            txtVereador2.Text = "";
            lblNomeVereador.Text = String.Empty;
            lblPartidoVereador.Text = String.Empty;
            picFotoVereador.Image = null;
        }

        private void LimparPrefeito()
        {
            txtPrefeito1.Text = "";
            txtPrefeito2.Text = "";
            lblNome.Text = String.Empty;
            lblPartido.Text = String.Empty;
            picFotoPrefeito.Image = null;
        }


        private void btnBranco_Click(object sender, EventArgs e)
        {
            if (etapaAtual == EtapaVotacao.Vereador)
            {
                etapaAtual = EtapaVotacao.Prefeito;
                panelVereador.Visible = false;
                panelPrefeito.Visible = true;
                LimparPrefeito();
            }
            else if (etapaAtual == EtapaVotacao.Prefeito)
            {
                FinalizarVotacao();
            }

            SoundPlayer s = new SoundPlayer(Properties.Resources.clique);
            s.Play();
        }



        private void btnCorrige_Click(object sender, EventArgs e)
        {
            if (etapaAtual == EtapaVotacao.Vereador)
            {
                LimparVereador();
            }
            else if (etapaAtual == EtapaVotacao.Prefeito)
            {
                LimparPrefeito();
            }

            SoundPlayer s = new SoundPlayer(Properties.Resources.clique);
            s.Play();
        }



        private void btnConfirma_Click(object sender, EventArgs e)
        {
            // Se estiver no painel de fim, reinicia a votação
            if (etapaAtual == EtapaVotacao.Fim)
            {
                IniciarNovaSessao();
                return;
            }

            // Votação para vereador
            if (etapaAtual == EtapaVotacao.Vereador)
            {
                if (string.IsNullOrEmpty(txtVereador1.Text) || string.IsNullOrEmpty(txtVereador2.Text))
                {
                    MessageBox.Show("Favor informar o candidato a vereador.");
                    return;
                }
                string numero = txtVereador1.Text + txtVereador2.Text;
                RegistrarVoto(votosVereador, numero);

                etapaAtual = EtapaVotacao.Prefeito;
                LimparPrefeito();
                AtualizarPainel();
                return;
            }

            // Votação para prefeito
            if (etapaAtual == EtapaVotacao.Prefeito)
            {
                if (string.IsNullOrEmpty(txtPrefeito1.Text) || string.IsNullOrEmpty(txtPrefeito2.Text))
                {
                    MessageBox.Show("Favor informar o candidato a prefeito.");
                    return;
                }
                string numero = txtPrefeito1.Text + txtPrefeito2.Text;
                RegistrarVoto(votosPrefeito, numero);

                FinalizarVotacao();
            }
        }
        private void RegistrarVoto(Dictionary<string, int> dicionario, string numero)
        {
            if (dicionario.ContainsKey(numero))
                dicionario[numero]++;
            else
                dicionario[numero] = 1;
        }

        private List<Candidato> vereadores = new List<Candidato>();
        private List<Candidato> prefeitos = new List<Candidato>();

        private void InicializarCandidatos()
        {
            vereadores.Add(new Candidato { Numero = "11", Nome = "Hermione Granger", Partido = "Cidadania" });
            vereadores.Add(new Candidato { Numero = "12", Nome = "Bruce Williams", Partido = "Trabalhista" });

            prefeitos.Add(new Candidato { Numero = "51", Nome = "Clark Kent", Partido = "Cidadania" });
            prefeitos.Add(new Candidato { Numero = "52", Nome = "Natasha Romanoff", Partido = "Trabalhista" });
        }



        private string ObterNomeCandidatoVereador(string numero)
        {
            // Simulado. Substitua por busca real em sua lista de candidatos
            if (numero == "12") return "Carlos Silva";
            if (numero == "34") return "Ana Souza";
            return "Desconhecido";
        }

        private string ObterNomeCandidatoPrefeito(string numero)
        {
            if (numero == "56") return "João Lima";
            if (numero == "78") return "Maria Rocha";
            return "Desconhecido";
        }

        private void AtualizarResultados()
        {
            string vencedorVereador = votosVereador.OrderByDescending(v => v.Value).FirstOrDefault().Key;
            string vencedorPrefeito = votosPrefeito.OrderByDescending(v => v.Value).FirstOrDefault().Key;

            // Você pode adicionar aqui a lógica para buscar nome e partido com base no número
            string nomeVereador = ObterNomeCandidatoVereador(vencedorVereador);
            string nomePrefeito = ObterNomeCandidatoPrefeito(vencedorPrefeito);

            VereadorResultado.Text = $"{nomeVereador} ({vencedorVereador})";
            PrefeitoResultado.Text = $"{nomePrefeito} ({vencedorPrefeito})";
        }
        private string ObterMaisVotado(Dictionary<string, int> votos, out int votosRecebidos)
        {
            string maisVotado = "Desconhecido";
            votosRecebidos = 0;

            foreach (var item in votos)
            {
                if (item.Value > votosRecebidos)
                {
                    maisVotado = item.Key;
                    votosRecebidos = item.Value;
                }
            }

            return maisVotado;
        }
        private string ObterNomeCandidato(List<Candidato> lista, string numero)
        {
            var candidato = lista.FirstOrDefault(c => c.Numero == numero);
            return candidato != null ? candidato.Nome : "Desconhecido";
        }

        private void MostrarResultados()
        {
            string vereadorMaisVotado = ObterMaisVotado(votosVereador, out int votosV);
            string prefeitoMaisVotado = ObterMaisVotado(votosPrefeito, out int votosP);

            // Supondo que você tem uma lista de candidatos com nome e número
            string nomeVereador = ObterNomeCandidato(vereadores, vereadorMaisVotado);
            string nomePrefeito = ObterNomeCandidato(prefeitos, prefeitoMaisVotado);

            VereadorResultado.Text = $"{nomeVereador} ({votosV})";
            PrefeitoResultado.Text = $"{nomePrefeito} ({votosP})";
        }


        private void FinalizarVotacao()
        {
            etapaAtual = EtapaVotacao.Fim;
            AtualizarResultados();
            AtualizarPainel();

            SoundPlayer s = new SoundPlayer(Properties.Resources.urna1);
            s.Play();
        }

        private void IniciarNovaSessao()
        {
            etapaAtual = EtapaVotacao.Vereador;
            LimparPrefeito();
            LimparVereador();
            AtualizarPainel();
        }


        private void PreencherPrefeito(string d1, string d2)
        {
            if (_dicPrefeito.ContainsKey(d1 + d2))
            {
                lblNome.Text = _dicPrefeito[d1 + d2].Nome;
                lblPartido.Text = _dicPrefeito[d1 + d2].Partido;
                picFotoPrefeito.Image = _dicPrefeito[d1 + d2].Foto;
            }
            else
            {
                MessageBox.Show("Prefeito não encontrado!");
            }
        }
        private void PreencherVereador(string d1, string d2)
        {
            string codigo = d1 + d2;
            if (_dicVereador.ContainsKey(codigo))
            {
                lblNomeVereador.Text = _dicVereador[codigo].Nome;
                lblPartidoVereador.Text = _dicVereador[codigo].Partido;
                picFotoVereador.Image = _dicVereador[codigo].Foto;
            }
            else
            {
                MessageBox.Show("Vereador não encontrado!");
            }
        }


        public class Candidato
        {
            public int Id { get; set; }
            public string Numero { get; set; }
            public string Nome { get; set; }
            public string Partido { get; set; }
            public Image Foto { get; set; }
        }

        private void panelVereador_Paint(object sender, PaintEventArgs e)
        {
            if (etapaAtual == EtapaVotacao.Vereador)
            {
                panelVereador.Visible = true;
            }
        }
    }
}
