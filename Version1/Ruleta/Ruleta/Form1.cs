using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InputKey;

//Powered By GatomanJuarez.

namespace Ruleta
{
    public partial class Form1 : Form
    {
        //Declaramos nuestras variables.
        //Lista para almacenar la informacion del txt al eliminar algun nombre.
        private List<string> personas = new List<string>();
        //Variable para almacenar el nombre nuevo, el nombre a eliminar y la ruta.
        private String nombre, eliminar, ruta = @"Datos.txt";
        //Variable para escribir en el txt.
        private System.IO.StreamWriter escritor;
        //Variable para leer el txt.
        private System.IO.StreamReader leerArchivo;
        //Variable para verificar que los datos introducidos no sean numeros y/o caracteres.
        private bool verificarN, verificarC;
        //Numero random.
        private Random random = new Random();
        private int auxiliar;

        //Metodos getters y setters.
        public void setpersonas(List<string> personas)
        {
            this.personas = personas;
        }

        public void setnombre(string nombre)
        {
            this.nombre = nombre;
        }

        public void seteliminar(string eliminar)
        {
            this.eliminar = eliminar;
        }

        public void setauxiliar(int auxiliar)
        {
            this.auxiliar = auxiliar;
        }

        public void setrandom(Random random)
        {
            this.random = random;
        }

        public void setruta(string ruta)
        {
            this.ruta = ruta;
        }

        public void setleerArcivo(System.IO.StreamReader leerArchivo)
        {
            this.leerArchivo = leerArchivo;
        }

        public void setescritor(System.IO.StreamWriter escritor)
        {
            this.escritor = escritor;
        }

        public void setverificarN(bool verificarN)
        {
            this.verificarN = verificarN;
        }

        public void setverificarC(bool verificarC)
        {
            this.verificarC = verificarC;
        }

        public bool getverificarN(bool verificarN)
        {
            return verificarN;
        }

        public int getauxiliar(int auxiliar)
        {
            return auxiliar;
        }

        public bool getverificarC(bool verificarC)
        {
            return verificarC;
        }

        public Random getrandom(Random random)
        {
            return random;
        }

        public List<string> getpersonas(List<string> personas)
        {
            return personas;
        }

        public string getnombre(string nombre)
        {
            return nombre;
        }

        public string geteliminar(string eliminar)
        {
            return eliminar;
        }

        public string getruta(string ruta)
        {
            return ruta;
        }

        public System.IO.StreamWriter getescritor(System.IO.StreamWriter escritor)
        {
            return escritor;
        }

        public System.IO.StringReader getleerArchivo(System.IO.StringReader leerArchivo)
        {
            return leerArchivo;
        }

        public Form1()
        {
            InitializeComponent();
            if (System.IO.File.Exists(ruta))
            {
                //Si existe el archivo.
                //Mostramos el contenido del txt.
                mostrar();
            }
            else
            {
                //Limpiamos nuestro label.
                lbMostrar.Text = "";
                //Creamos el archivo (en caso de que no exista).
                crearArchivo();
            }
        }

        //Verificamos si el contenido contiene algun numero.
        public bool contieneNumero(string Expression)
        {
            bool isNum;
            string[] caracteres = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (byte i = 0; i < caracteres.Length; i++)
            {
                if (Expression.Contains(caracteres[i]))
                {
                    isNum = true;
                    return isNum;
                }
            }
            isNum = false;
            return isNum;
        }

        //Verificamos si el contenido contiene algun caracter.
        public bool contieneCaracter(string Expression)
        {
            bool isChar;
            Char[] caracteres = {'*', '/', '-', '+', '.', '|', '°', '¬', '!', '"', '#', '$', '%', '&', '(', ')', '=', '?','¡', '´', '¨', '*',
                                 '~', '{','[' ,'^', ']', '}','`', ',','.',';','-','_' };
            for (byte i = 0; i < caracteres.Length; i++)
            {
                if (Expression.Contains(caracteres[i]))
                {
                    isChar = true;
                    return isChar;
                }
            }
            isChar = false;
            return isChar;
        }

        private void agregarPersonas()
        {
            //Try para capturar excepciones.
            try
            {
                //Pedimos y almacenamos el nuevo nombre.
                nombre = InputDialog.mostrar("Introduzca el nuevo nombre.");
                //Verificamos que no contenga numeros.
                verificarN = contieneNumero(nombre);
                verificarC = contieneCaracter(nombre);
                //En caso de contener alguno no lo guardamos.
                if (verificarN || verificarC)
                {
                    //Si contiene algun numero o caracter mostramos el mensaje.
                    MessageBox.Show("Introduca solo texto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //En caso de que no lo sea usamos nuestro objeto para escribir el nombre.
                    using (escritor = System.IO.File.AppendText(ruta))
                    {
                        //Escribimos en nuestro txt el nombre.
                        escritor.WriteLine(nombre);
                    }
                }
            }
            catch (Exception)
            {
                //Mostramos algun error en caso de que se tenga.
                MessageBox.Show("Introduca solo texto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crearArchivo()
        {
            //Usamos nuestro objeto para crear el archivo.
            using (escritor = new System.IO.StreamWriter(ruta))
            {
                //Escribimos un pequeño titulo.
                escritor.WriteLine("Almacenamiento de nombres:");
            }
        }

        private void mostrar()
        {
            try
            {
                //Declaramos una variable auxiliar.
                string linea;
                //Limpiamos nuestro label.
                lbMostrar.Text = "";
                //Usamos nuestro objeto para leer el txt.
                using (leerArchivo = new System.IO.StreamReader(ruta))
                {
                    //Usamos el while hasta que no encentre mas lienas.
                    while (leerArchivo.Peek() > -1)
                    {
                        //Todo lo escrito en las lineas las guardamos en nuestra variable.
                        linea = leerArchivo.ReadLine();
                        //Comprovamos que la variable tenga algun valor.
                        if (!String.IsNullOrEmpty(linea))
                        {
                            //Lo imprimimos en nuestro label.
                            lbMostrar.Text += linea + "\n";
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "Ha ocurrido un error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarPersona()
        {
            try
            {
                //Guardamos el nombre a eliminar.
                eliminar = InputDialog.mostrar("Introduzca el nombre \n completo para eliminar.", "Dato");
                //Verificamos que no contenga numeros.
                verificarN = contieneNumero(nombre);
                verificarC = contieneCaracter(nombre);
                //En caso de contener alguno no lo guardamos.
                if (verificarN || verificarC)
                {
                    //Si contiene algun numero o caracter mostramos el mensaje.
                    MessageBox.Show("Introduca solo texto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (leerArchivo = new System.IO.StreamReader(ruta))
                    {
                        while (leerArchivo.Peek() > -1)
                        {
                            string linea = leerArchivo.ReadLine();
                            if (!String.IsNullOrEmpty(linea))
                            {
                                //Lo que contiene el archivo que lo abrimos en modo lectura lo guardamos en una lista.
                                personas.Add(linea);
                            }
                        }
                    }
                    //Preguntamos si en la lista esta nuestro nombre a eliminar.
                    if (personas.Contains(eliminar))
                    {
                        //Y si esta lo eliminamos.
                        personas.Remove(eliminar);
                    }
                    else
                    {
                        //Mostramos un mensaje en caso de que no.
                        MessageBox.Show("No se ha encontrado el \n nombre a eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //Esta lisa la guardamos en nuestro txt.
                    using (escritor = new System.IO.StreamWriter(ruta))
                    {
                        foreach (string contenido in personas)
                        {
                            escritor.WriteLine(contenido);
                        }
                    }

                    //Llamamos a nuestro metodo para observar cambios.
                    mostrar();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "Introduca solo texto.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void generar()
        {
            try
            {
                personas.Clear();

                using (leerArchivo = new System.IO.StreamReader(ruta))
                {
                    while (leerArchivo.Peek() > -1)
                    {
                        string linea = leerArchivo.ReadLine();
                        if (!String.IsNullOrEmpty(linea))
                        {
                            //Lo que contiene el archivo que lo abrimos en modo lectura lo guardamos en una lista.
                            personas.Add(linea);
                        }
                    }
                }
                //Generamos numero random.
                auxiliar = random.Next(0, personas.Count);
                //Lo imprimimos.
                lbMostrar.Text = personas[auxiliar];
            }
            catch (Exception)
            {
                MessageBox.Show("Error", "Introduca solo texto.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            lbMostrar.Text = "";
            generar();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            escritor.Close();
            leerArchivo.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            agregarPersonas();
            lbMostrar.Text = "";
            mostrar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarPersona();
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            lbMostrar.Text = "";
            mostrar();
        }

    }
}
