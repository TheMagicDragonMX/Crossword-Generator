using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Crucigrama_2._0
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        //VARIABLES GLOBALES
        string Palabra; //Palabra que se extraera del archivo, y que se generara en el crucigrama
        string Pista; //Pista respecto a la palabra
        public static string Descripcion = ""; //Conjunto de pistas para mostrar al usuario
        string Letra; //Guardara la letra que se selecciono al azar del crucigrama ya generado

        int Numero_De_Letra; //Guarda el indice de la TextBox con una letra en especifico escogida aleatoriamente
        int Total_De_Lineas = File.ReadAllLines("Words.txt").Length; //Guarda cuantas palabras hay en el archivo
        int Numero_De_Palabra; //Numero que se genero para escoger la palabra y la pista
        int X, Y; //Posiciones de cada TextBox
        int Numero_de_Pista = 1; //Aumentara cada vez que se escriba una palabra en el archivo
        int SEG_Escojer_Letra; //Detectara cuando una letra no la posee ninguna palabra restante
        int Desplazamiento; //Guarda que tanto hay que desplazarse en la direccion opuesta para converger la letra

        bool AlliSiPuedeHaberUnaPalabra = false; //Verifica si en un lugar escogido se puede eescribir una palabra
        int Vertical = 2; //Determina si la palabra se orientara hacia arriba o hacia los lados

        //LISTAS
        //Declarar lista de TextBoxes
        List<TextBox> Lista_Textbox = new List<TextBox>();

        //Declarar lista de Strings (Palabras)
        List<string> Palabras = new List<string>();

        //Declarar lista de String (Pistas)
        List<string> Pistas = new List<string>();

        //Declarar lista de Labels
        List<Label> Labels = new List<Label>();
        
        private void Form3_Load(object sender, EventArgs e)
        {
            //ARCHIVOS
            StreamReader Words = new StreamReader("Words.txt");
            StreamReader Clues = new StreamReader("Clues.txt");

            //Declarar numeros aleatorios
            Random Random = new Random();

            //Asingar a las listas de pistas y palabras los datos de los archivos
            for (int Contador = 0; Contador < Total_De_Lineas; Contador++)
                Palabras.Add(Words.ReadLine()); //Lee y guarda cada linea del archivo de palabras

            for (int Contador = 0; Contador < Total_De_Lineas; Contador++)
                Pistas.Add(Clues.ReadLine()); //Lee y guarda cada linea del archivo de pistas

            //Generar una primera palabra, que sera la base de creacion del resto de las palabras
            Numero_De_Palabra = Random.Next(0, Palabras.Count); //Numero para escojer palabra y pista

            Palabra = Palabras[Numero_De_Palabra]; //Palabra aleatoria del archivo
            Palabras.Remove(Palabra); //Remover palabra generada para evitar repeticion

            Pista = Pistas[Numero_De_Palabra]; //Pista respecto a la palabra
            Pistas.Remove(Pista); //Remover pista para evitar repeticion

            Descripcion += Numero_de_Pista.ToString() + ".- " + Pista + "\n"; //Añadir a la descripcion

            //Generar palabra en la ventana
            X = 100; //Coordenada X = 100 de la primera palabra
            for(int Contador = 0; Contador < Palabra.Length; Contador++)
            {
                //Propiedades de cada TextBox generada
                TextBox Caja = new TextBox();
                Caja.Location = new Point(X, 100);
                Caja.Multiline = true;
                Caja.MaxLength = 1;
                Font TipoDeLetra = new Font("Times New Roman", 12);
                Caja.Font = TipoDeLetra;
                Caja.TextAlign = HorizontalAlignment.Center;
                Caja.Size = new Size(25, 25);

                //Lo que hace a cada TextBox especial
                Caja.Name = Palabra.Substring(Contador, 1); //La caja lleva el nombre de la letra que esta deberia tener
                //Caja.Text = Palabra.Substring(Contador, 1); //TEMPORAL: Verificar la creacion del crucigrama

                Controls.Add(Caja); //Añadir nueva TextBox a la Form
                Lista_Textbox.Add(Caja); //Añadir nueva TextBox a la lista

                X += 25; //Hacer que la coordenada cambie para la siguiente TextBox a generar 
            }

            //Al terminar de generar la palabra, generara que numero de palabra fue
            X = X - 25 - (Palabra.Length * 25);
            Label Num_Palabra = new Label();
            Num_Palabra.Location = new Point(X, 100);
            Num_Palabra.ForeColor = Color.White;
            Num_Palabra.BackColor = Color.Transparent;
            Font Tipo_De_Letra = new Font("Times New Roman", 12);
            Num_Palabra.Font = Tipo_De_Letra;
            Num_Palabra.Size = new Size(25, 25);
            Num_Palabra.TextAlign = ContentAlignment.MiddleCenter;
            Num_Palabra.Text = Numero_de_Pista.ToString() + "-";
            Labels.Add(Num_Palabra); //Añadir a la Lista de Labels
            Controls.Add(Num_Palabra); //Mostrar la label en el Form

            /////HASTA AQUI TERMINA LA GENERACION DE LA PALABRA BASE, AHORA EMPIEZA LA REPETICION DE GENERACION DE PALABRAS/////

            for (int Contador = 1; Contador < Form1.Dificultad; Contador++) //Repite el numero aleatorio de palabras que se genero
            {
                Numero_De_Letra = Random.Next(1, Lista_Textbox.Count - 1); //Escoje el indice de alguna TextBox ya generada
                Letra = Lista_Textbox[Numero_De_Letra].Name; //Guarda la letra escogida aleatoriamente

                //Busca una palabra en el archivo que tenga la letra escogida
                SEG_Escojer_Letra = 0; //SEGURIDAD
                while (true) 
                {
                    Numero_De_Palabra = Random.Next(0, Palabras.Count); //Escoje el indice de una palabra aleatoria
                    Palabra = Palabras[Numero_De_Palabra]; //Guarda la palabra escojiga aleatoriamente

                    if (Palabra.Contains(Letra)) break; //Si en la palabra generada aleatoriamente esta la letra seleccionada, sale del ciclo

                    SEG_Escojer_Letra++; //Va subiendo segun que tantas veces no se encontro una letra en todac la Lista de Palabras
                    if (SEG_Escojer_Letra == Palabras.Count) //Si la variable de seguridad alcanzo un limite de busqueda de letra, se escojera una letra diferente
                    {
                        Numero_De_Letra = Random.Next(1, Lista_Textbox.Count - 1); //Escoje el indice de alguna TextBox ya generada
                        Letra = Lista_Textbox[Numero_De_Letra].Name; //Guarda la letra escogida aleatoriamente
                        SEG_Escojer_Letra = 0; //Reiniciar conteo
                    } //Seguridad al escojer letra

                } //Buscar una palabra que contenga la letra escojida

                //Encontrar el desplazamiento de la letra
                Desplazamiento = Averiguar_Desplazamiento(Palabra, Letra); //Encontrar el desplazamiento de la letra

                //Averiguar cual sera el sentido de la palabra (Hacia los lados o hacia abajo)
                Vertical = 2;
                while(Vertical == 2)
                {
                    foreach(TextBox Sospechoso in Lista_Textbox)
                        if ((Lista_Textbox[Numero_De_Letra].Location.X + 25 == Sospechoso.Location.X || Lista_Textbox[Numero_De_Letra].Location.X - 25 == Sospechoso.Location.X) && Lista_Textbox[Numero_De_Letra].Location.Y == Sospechoso.Location.Y)
                            { Vertical = 1; break; } //No se puede escribir en horizontal
                    
                    foreach (TextBox Sospechoso in Lista_Textbox)
                        if ((Lista_Textbox[Numero_De_Letra].Location.Y + 25 == Sospechoso.Location.Y || Lista_Textbox[Numero_De_Letra].Location.Y - 25 == Sospechoso.Location.Y) && Lista_Textbox[Numero_De_Letra].Location.X == Sospechoso.Location.X)
                            { Vertical = 0; break; } //No se puede escribir en vertical

                    if (Vertical == 1 || Vertical == 0) break; //Salir del ciclo

                    Vertical = 2;
                    //La palabra no puede ser escrita en ese lugar y hay que buscar otro
                    Numero_De_Letra = Random.Next(1, Lista_Textbox.Count - 1); //Escoje el indice de alguna TextBox ya generada
                    Letra = Lista_Textbox[Numero_De_Letra].Name; //Guarda la letra escogida aleatoriamente

                    //Busca una palabra en el archivo que tenga la letra escogida
                    SEG_Escojer_Letra = 0; //SEGURIDAD
                    while (true)
                    {
                        Numero_De_Palabra = Random.Next(0, Palabras.Count); //Escoje el indice de una palabra aleatoria
                        Palabra = Palabras[Numero_De_Palabra]; //Guarda la palabra escojiga aleatoriamente

                        if (Palabra.Contains(Letra)) break; //Si en la palabra generada aleatoriamente esta la letra seleccionada, sale del ciclo

                        SEG_Escojer_Letra++; //Va subiendo segun que tantas veces no se encontro una letra en todac la Lista de Palabras
                        if (SEG_Escojer_Letra == Palabras.Count) //Si la variable de seguridad alcanzo un limite de busqueda de letra, se escojera una letra diferente
                        {
                            Numero_De_Letra = Random.Next(1, Lista_Textbox.Count - 1); //Escoje el indice de alguna TextBox ya generada
                            Letra = Lista_Textbox[Numero_De_Letra].Name; //Guarda la letra escogida aleatoriamente
                            SEG_Escojer_Letra = 0; //Reiniciar conteo
                        } //Seguridad al escojer letra
                    } //Buscar una palabra que contenga la letra escojida
                }

                //Validar que la palabra puede ser escrita en ese lugar
                while (AlliSiPuedeHaberUnaPalabra == false)
                {
                    AlliSiPuedeHaberUnaPalabra = Validar_Si_Puede_Haber_Palabra(Vertical, Palabra, Desplazamiento); //Verificar si es posible escribir la palabra alli

                    if(AlliSiPuedeHaberUnaPalabra == false)
                    { //Cambiar de letra, y de palabra
                        Numero_De_Letra = Random.Next(1, Lista_Textbox.Count - 1); //Escoje el indice de alguna TextBox ya generada
                        Letra = Lista_Textbox[Numero_De_Letra].Name; //Guarda la letra escogida aleatoriamente

                        //Busca una palabra en el archivo que tenga la letra escogida
                        SEG_Escojer_Letra = 0; //SEGURIDAD
                        while (true)
                        {
                            Numero_De_Palabra = Random.Next(0, Palabras.Count); //Escoje el indice de una palabra aleatoria
                            Palabra = Palabras[Numero_De_Palabra]; //Guarda la palabra escojiga aleatoriamente

                            if (Palabra.Contains(Letra)) break; //Si en la palabra generada aleatoriamente esta la letra seleccionada, sale del ciclo

                            SEG_Escojer_Letra++; //Va subiendo segun que tantas veces no se encontro una letra en todac la Lista de Palabras
                            if (SEG_Escojer_Letra == Palabras.Count) //Si la variable de seguridad alcanzo un limite de busqueda de letra, se escojera una letra diferente
                            {
                                Numero_De_Letra = Random.Next(1, Lista_Textbox.Count - 1); //Escoje el indice de alguna TextBox ya generada
                                Letra = Lista_Textbox[Numero_De_Letra].Name; //Guarda la letra escogida aleatoriamente
                                SEG_Escojer_Letra = 0; //Reiniciar conteo
                            } //Seguridad al escojer letra

                        } //Buscar una palabra que contenga la letra escojida

                        //Generar la palabra en el Form
                        Desplazamiento = Averiguar_Desplazamiento(Palabra, Letra); //Encontrar el desplazamiento de la letra

                        Vertical = 2;
                        //Averiguar cual sera el sentido de la palabra (Hacia los lados o hacia abajo)
                        while (Vertical == 2)
                        {
                            foreach (TextBox Sospechoso in Lista_Textbox)
                                if ((Lista_Textbox[Numero_De_Letra].Location.X + 25 == Sospechoso.Location.X || Lista_Textbox[Numero_De_Letra].Location.X - 25 == Sospechoso.Location.X) && Lista_Textbox[Numero_De_Letra].Location.Y == Sospechoso.Location.Y)
                                { Vertical = 1; break; } //No se puede escribir en horizontal

                            foreach (TextBox Sospechoso in Lista_Textbox)
                                if ((Lista_Textbox[Numero_De_Letra].Location.Y + 25 == Sospechoso.Location.Y || Lista_Textbox[Numero_De_Letra].Location.Y - 25 == Sospechoso.Location.Y) && Lista_Textbox[Numero_De_Letra].Location.X == Sospechoso.Location.X)
                                { Vertical = 0; break; } //No se puede escribir en vertical

                            if (Vertical == 1 || Vertical == 0) break; //Salir del ciclo

                            Vertical = 2;
                            //La palabra no puede ser escrita en ese lugar y hay que buscar otro
                            Numero_De_Letra = Random.Next(1, Lista_Textbox.Count - 1); //Escoje el indice de alguna TextBox ya generada
                            Letra = Lista_Textbox[Numero_De_Letra].Name; //Guarda la letra escogida aleatoriamente

                            //Busca una palabra en el archivo que tenga la letra escogida
                            SEG_Escojer_Letra = 0; //SEGURIDAD
                            while (true)
                            {
                                Numero_De_Palabra = Random.Next(0, Palabras.Count); //Escoje el indice de una palabra aleatoria
                                Palabra = Palabras[Numero_De_Palabra]; //Guarda la palabra escojiga aleatoriamente

                                if (Palabra.Contains(Letra)) break; //Si en la palabra generada aleatoriamente esta la letra seleccionada, sale del ciclo

                                SEG_Escojer_Letra++; //Va subiendo segun que tantas veces no se encontro una letra en todac la Lista de Palabras
                                if (SEG_Escojer_Letra == Palabras.Count) //Si la variable de seguridad alcanzo un limite de busqueda de letra, se escojera una letra diferente
                                {
                                    Numero_De_Letra = Random.Next(1, Lista_Textbox.Count - 1); //Escoje el indice de alguna TextBox ya generada
                                    Letra = Lista_Textbox[Numero_De_Letra].Name; //Guarda la letra escogida aleatoriamente
                                    SEG_Escojer_Letra = 0; //Reiniciar conteo
                                } //Seguridad al escojer letra
                            } //Buscar una palabra que contenga la letra escojida
                        }

                    } //Cambiar de letra, y de palabra
                } //Validar que la palabra puede ser escrita en ese lugar

                //Crear la palabra
                if(Vertical == 1) //La palabra se creara en vertical
                {
                    Y = Lista_Textbox[Numero_De_Letra].Location.Y - (Desplazamiento + 1) * 25; //Punto de partida de Y

                    //Generar label que indica numero de palabra
                    Numero_de_Pista++;
                    Label Num_Palabra_X = new Label();
                    Num_Palabra_X.Location = new Point(Lista_Textbox[Numero_De_Letra].Location.X, Y);
                    Num_Palabra_X.Font = Tipo_De_Letra;
                    Num_Palabra_X.ForeColor = Color.White;
                    Num_Palabra_X.BackColor = Color.Transparent;
                    Num_Palabra_X.Size = new Size(25, 25);
                    Num_Palabra_X.TextAlign = ContentAlignment.MiddleCenter;
                    Num_Palabra_X.Text = Numero_de_Pista.ToString() + "-";
                    Labels.Add(Num_Palabra_X); //Añadir a la Lista de Labels
                    Controls.Add(Num_Palabra_X); //Mostrar la label en el Form
                    Y += 25;

                    //Descripciones
                    Pista = Pistas[Numero_De_Palabra]; //Pista respecto a la palabra
                    Pistas.Remove(Pista); //Remover pista para evitar repeticion
                    Descripcion += Numero_de_Pista.ToString() + ".- " + Pista + "\n"; //Añadir a la descripcion

                    for (int Contador_2 = 0; Contador_2 < Palabra.Length; Contador_2++)
                    {
                        //Propiedades de cada TextBox generada
                        TextBox Caja = new TextBox();
                        Caja.Location = new Point(Lista_Textbox[Numero_De_Letra].Location.X, Y);
                        Caja.Multiline = true;
                        Caja.Font = Tipo_De_Letra;
                        Caja.MaxLength = 1;
                        Caja.TextAlign = HorizontalAlignment.Center;
                        Caja.Size = new Size(25, 25);

                        //Lo que hace a cada TextBox especial
                        Caja.Name = Palabra.Substring(Contador_2, 1); //La caja lleva el nombre de la letra que esta deberia tener
                        //Caja.Text = Palabra.Substring(Contador_2, 1); //TEMPORAL: Verificar la creacion del crucigrama

                        Controls.Add(Caja); //Añadir nueva TextBox a la Form
                        Lista_Textbox.Add(Caja); //Añadir nueva TextBox a la lista

                        Y += 25; //Hacer que la coordenada cambie para la siguiente TextBox a generar
                    }
                } //La palabra se creara en vertical 
                else
                { //La palabra se creara en horizontal
                    X = Lista_Textbox[Numero_De_Letra].Location.X - (Desplazamiento + 1) * 25; //Punto de partida de Y

                    //Generar label que indica numero de palabra
                    Numero_de_Pista++;
                    Label Num_Palabra_X = new Label();
                    Num_Palabra_X.Location = new Point(X, Lista_Textbox[Numero_De_Letra].Location.Y);
                    Num_Palabra_X.Font = Tipo_De_Letra;
                    Num_Palabra_X.ForeColor = Color.White;
                    Num_Palabra_X.BackColor = Color.Transparent;
                    Num_Palabra_X.Size = new Size(25, 25);
                    Num_Palabra_X.TextAlign = ContentAlignment.MiddleCenter;
                    Num_Palabra_X.Text = Numero_de_Pista.ToString() + "-";
                    Labels.Add(Num_Palabra_X); //Añadir a la Lista de Labels
                    Controls.Add(Num_Palabra_X); //Mostrar la label en el Form
                    X += 25;

                    //Descripciones
                    Pista = Pistas[Numero_De_Palabra]; //Pista respecto a la palabra
                    Pistas.Remove(Pista); //Remover pista para evitar repeticion
                    Descripcion += Numero_de_Pista.ToString() + ".- " + Pista + "\n"; //Añadir a la descripcion

                    for (int Contador_2 = 0; Contador_2 < Palabra.Length; Contador_2++)
                    {
                        //Propiedades de cada TextBox generada
                        TextBox Caja = new TextBox();
                        Caja.Location = new Point(X, Lista_Textbox[Numero_De_Letra].Location.Y);
                        Caja.Multiline = true;
                        Font TipoDeLetra = new Font("Times New Roman", 12);
                        Caja.Font = TipoDeLetra;
                        Caja.MaxLength = 1;
                        Caja.TextAlign = HorizontalAlignment.Center;
                        Caja.Size = new Size(25, 25);

                        //Lo que hace a cada TextBox especial
                        Caja.Name = Palabra.Substring(Contador_2, 1); //La caja lleva el nombre de la letra que esta deberia tener
                        //Caja.Text = Palabra.Substring(Contador_2, 1); //TEMPORAL: Verificar la creacion del crucigrama

                        Controls.Add(Caja); //Añadir nueva TextBox a la Form
                        Lista_Textbox.Add(Caja); //Añadir nueva TextBox a la lista

                        X += 25; //Hacer que la coordenada cambie para la siguiente TextBox a generar
                    }
                } //La palabra se creara en vertical

                Palabras.Remove(Palabra); //Remover palabra generada para evitar repeticion
                AlliSiPuedeHaberUnaPalabra = false; //Reiniciar la validacion
                Vertical = 2; //Reiniciar orientacion
            } //For creador de palabras

            Valideichon.Start();

            Win.Visible = false;

            //FORMS
            Form4 Definiciones = new Form4();
            Definiciones.Show();

        } //Form3.Load

        public int Averiguar_Desplazamiento(string Palabra, string Letra)
        {
            //Recorre cada letra de la palabra hasta encontrar la letra deseada
            for (int Contador = 0; Contador < Palabra.Length; Contador++)
            {
                if (Palabra.Substring(Contador, 1) == Letra)
                    return Contador;
            } //Recorre cada letra de la palabra hasta encontrar la letra deseada
            return 0;
        } //Averiguar_Desplazamiento

        private void Valideichon_Tick(object sender, EventArgs e)
        {
            foreach (TextBox Correcto in Lista_Textbox)
            {
                if (Correcto.Name == Correcto.Text)
                {
                    Correcto.BackColor = Color.PaleGreen;
                } 
                else
                    if (Correcto.Text == null || Correcto.Text == "")
                    Correcto.BackColor = Color.LightGray;
                else
                    Correcto.BackColor = Color.Salmon;

                Correcto.Text = Correcto.Text.ToUpper();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Valideichon.Stop();

            //FORMS
            Form4 Definiciones = new Form4();

            Definiciones.Close();

            //Reiniciar las variables
            Palabra = null;
            Pista = null; 
            Descripcion = ""; //Conjunto de pistas para mostrar al usuario
            Letra = null; //Guardara la letra que se selecciono al azar del crucigrama ya generado
            Vertical = 2; //Determina si la palabra se orientara hacia arriba o hacia los lados
            Numero_de_Pista = 1;
            Numero_De_Palabra = 0;
            Desplazamiento = 0;

            //Vaciar listas
            Controls.Clear();
            Lista_Textbox.Clear();
            Labels.Clear();
            Palabras.Clear();
            Pistas.Clear();

            Form1.Dificultad = 0;
            this.Close();
        }

        public bool Validar_Si_Puede_Haber_Palabra(int Vertical, string Palabra, int Desplazamiento)
        {
            //#1.- Validar si no hay nada que bloquee a la palabra antes de la letra escogida (si hay desplazamiento)
            if (Desplazamiento != 0)
            {
                if(Vertical == 1)
                { //Verificara elementos en vertical
                    for (int Contador = 1; Contador <= Desplazamiento + 1; Contador++) //Verifica por coordenadas verticales
                    {
                        foreach (TextBox Sospechoso in Lista_Textbox) //Verifica la posicion de cada TextBox
                            if ((Lista_Textbox[Numero_De_Letra].Location.Y - (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X == Sospechoso.Location.X) || (Lista_Textbox[Numero_De_Letra].Location.Y - (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X + 25 == Sospechoso.Location.X) || (Lista_Textbox[Numero_De_Letra].Location.Y - (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X - 25 == Sospechoso.Location.X) || Lista_Textbox[Numero_De_Letra].Location.Y - (Desplazamiento + 2) * 25 < 0)
                                return false;

                        foreach (Label Sospechoso in Labels) //Verifica la posicion de cada Label
                            if ((Lista_Textbox[Numero_De_Letra].Location.Y - (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X == Sospechoso.Location.X) || (Lista_Textbox[Numero_De_Letra].Location.Y - (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X + 25 == Sospechoso.Location.X) || (Lista_Textbox[Numero_De_Letra].Location.Y - (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X - 25 == Sospechoso.Location.X) || Lista_Textbox[Numero_De_Letra].Location.Y - (Desplazamiento + 2) * 25 < 0)
                                return false;
                    } //For que recorre coordenadas
                } //Verificara elementos en vertical
                else
                { //Verificara elementos en horizontal
                    for (int Contador = 1; Contador <= Desplazamiento + 1; Contador++) //Verifica por coordenadas horizontales
                    {
                        foreach (TextBox Sospechoso in Lista_Textbox) //Verifica la posicion de cada TextBox
                            if ((Lista_Textbox[Numero_De_Letra].Location.X - (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y == Sospechoso.Location.Y) || (Lista_Textbox[Numero_De_Letra].Location.X - (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y + 25 == Sospechoso.Location.Y) || (Lista_Textbox[Numero_De_Letra].Location.X - (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y - 25 == Sospechoso.Location.Y) || Lista_Textbox[Numero_De_Letra].Location.X - (Desplazamiento + 2) * 25 < 0)
                                return false;

                        foreach (Label Sospechoso in Labels) //Verifica la posicion de cada Label
                            if ((Lista_Textbox[Numero_De_Letra].Location.X - (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y == Sospechoso.Location.Y) || (Lista_Textbox[Numero_De_Letra].Location.X - (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y + 25 == Sospechoso.Location.Y) || (Lista_Textbox[Numero_De_Letra].Location.X - (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y - 25 == Sospechoso.Location.Y) || Lista_Textbox[Numero_De_Letra].Location.X - (Desplazamiento + 2) * 25 < 0)
                                return false;
                    }
                } //Verificara elementos en horizontal

            } //#1.- Validar si no hay nada que bloquee a la palabra antes de la letra escogida (si hay desplazamiento)

            //#2.- Validar si no hay nada que bloquee a la palabre despues de la letra escogida (si hay o no desplazamiento)
            if (Vertical == 1)
            { //Verificara elementos en vertical
                for (int Contador = 1; Contador <= Palabra.Length - Desplazamiento; Contador++) //Verifica por coordenadas verticales
                {
                    foreach (TextBox Sospechoso in Lista_Textbox) //Verifica la posicion de cada TextBox
                        if ((Lista_Textbox[Numero_De_Letra].Location.Y + (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X == Sospechoso.Location.X) || (Lista_Textbox[Numero_De_Letra].Location.Y + (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X + 25 == Sospechoso.Location.X) || (Lista_Textbox[Numero_De_Letra].Location.Y + (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X - 25 == Sospechoso.Location.X))
                            return false;

                    foreach (Label Sospechoso in Labels) //Verifica la posicion de cada TextBox
                        if ((Lista_Textbox[Numero_De_Letra].Location.Y + (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X == Sospechoso.Location.X) || (Lista_Textbox[Numero_De_Letra].Location.Y + (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X + 25 == Sospechoso.Location.X) || (Lista_Textbox[Numero_De_Letra].Location.Y + (Contador * 25) == Sospechoso.Location.Y && Lista_Textbox[Numero_De_Letra].Location.X - 25 == Sospechoso.Location.X))
                            return false;
                } //For que recorre coordenadas
            } //Verificara elementos en vertical
            else
            { //Verificara elementos en horizontal
                for (int Contador = 1; Contador <= Palabra.Length - Desplazamiento; Contador++) //Verifica por coordenadas horizontales
                {
                    foreach (TextBox Sospechoso in Lista_Textbox) //Verifica la posicion de cada TextBox
                        if ((Lista_Textbox[Numero_De_Letra].Location.X + (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y == Sospechoso.Location.Y) || (Lista_Textbox[Numero_De_Letra].Location.X + (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y + 25 == Sospechoso.Location.Y) || (Lista_Textbox[Numero_De_Letra].Location.X + (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y - 25 == Sospechoso.Location.Y))
                            return false;

                    foreach (Label Sospechoso in Labels) //Verifica la posicion de cada TextBox
                        if ((Lista_Textbox[Numero_De_Letra].Location.X + (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y == Sospechoso.Location.Y) || (Lista_Textbox[Numero_De_Letra].Location.X + (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y + 25 == Sospechoso.Location.Y) || (Lista_Textbox[Numero_De_Letra].Location.X + (Contador * 25) == Sospechoso.Location.X && Lista_Textbox[Numero_De_Letra].Location.Y - 25 == Sospechoso.Location.Y))
                            return false;
                }
            } //Verificara elementos en horizontal

            return true;
        }

    } //Area de trabajo
}
