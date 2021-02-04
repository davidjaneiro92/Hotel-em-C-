using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaHotel
{
    static class Program
    {

        // declarodo as variaveis globais do sistema 
        public static String nomeUsuario;
        public static String cargoUsuario;

        public static String ChamadaProdutos;
       
        public static String nomeProduto;
        public static String estoqueProduto;
        public static String idProduto;

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}
