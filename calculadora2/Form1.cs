using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculadora2
{
    public partial class Form1 : Form
    {
        public decimal Resultado { get; set; }
        public decimal Valor { get; set; }
        private Operacao OperacaoSelecionada {  get; set; }

        private enum Operacao
        {
            Adicao,
            Subtracao,
            Multiplicacao,
            Divisao
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtresultado.Text += "9";
        }

        private void btnadicao_Click(object sender, EventArgs e)
        {
            OperacaoSelecionada = Operacao.Adicao;
            Valor = Convert.ToDecimal(txtresultado.Text);
            txtresultado.Text = "";
            lblOperacao.Text = "+";
        }

        private void btnsubtracao_Click(object sender, EventArgs e)
        {
            OperacaoSelecionada = Operacao.Subtracao;
            Valor = Convert.ToDecimal(txtresultado.Text);
            txtresultado.Text = "";
            lblOperacao.Text = "-";
        }

        private void btnmulti_Click(object sender, EventArgs e)
        {
            OperacaoSelecionada = Operacao.Multiplicacao;
            Valor = Convert.ToDecimal(txtresultado.Text);
            txtresultado.Text = "";
            lblOperacao.Text = "X";
        }

        private void btndivi_Click(object sender, EventArgs e)
        {
            OperacaoSelecionada = Operacao.Divisao;
            Valor = Convert.ToDecimal(txtresultado.Text);
            txtresultado.Text = "";
            lblOperacao.Text = "/";
        }

        private void btnigual_Click(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            switch (OperacaoSelecionada)
            {
                case Operacao.Adicao:
                    Resultado = Valor + Convert.ToDecimal(txtresultado.Text);
                    break;

                case Operacao.Subtracao:
                    Resultado = Valor - Convert.ToDecimal(txtresultado.Text);
                    break;
                case Operacao.Multiplicacao:
                    Resultado = Valor * Convert.ToDecimal(txtresultado.Text);
                    break;
                case Operacao.Divisao:
                    Resultado = Valor / Convert.ToDecimal(txtresultado.Text);
                    break;
            }
            txtresultado.Text =Convert.ToString(Resultado);
            lblOperacao.Text = "=";
        }

        private void btnvirgula_Click(object sender, EventArgs e)
        {
            if (!txtresultado.Text.Contains(",")) ;
            txtresultado.Text += ",";

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtresultado.Text = "";
            lblOperacao.Text = "";
        }

        private void txtresultado_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
