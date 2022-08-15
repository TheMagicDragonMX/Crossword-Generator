using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crucigrama_2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Variable que guarda el numero de palabras a generar
        public static int Dificultad;

        //Declarar numeros aleatorios
        Random Random = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Dificultad = Random.Next(2, 5); //Numero de palabras (Facil)
            //Declarar nueva Form
            Form3 Juego = new Form3();
            Juego.Show(); //Abrir nueva form
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Dificultad = Random.Next(6, 10); //Numero de palabras (Normal)
            //Declarar nueva Form
            Form3 Juego = new Form3();
            Juego.Show(); //Abrir nueva form
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Dificultad = Random.Next(10, 15); //Numero de palabras (Dificil)
            //Declarar nueva Form
            Form3 Juego = new Form3();
            Juego.Show(); //Abrir nueva form
        }
    }
}
