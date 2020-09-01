using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Gabriel
{
    public class ManipuladorArquivos
    {

        private static string EnderecoArquivo = AppDomain.CurrentDomain.BaseDirectory + "contatos.txt"; //Endereço do arquivo
        public static List<Contato> LerArquivo()
        {
            List<Contato> contatosList = new List<Contato>();
            if (File.Exists(@EnderecoArquivo)) // Verifica se o arquivo existe
            {
                using (StreamReader sr = File.OpenText(@EnderecoArquivo)) //Para leitura do arquivo
                {
                    while (sr.Peek() >= 0) // Peek() Quando não existe linha a ser lida retorna -1
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';'); //Split() Quebra a linha em caracteres especificados
                        if (linhaComSplit.Count() == 3) // Verifica se o tamanho do array é 3
                        {
                            Contato contato = new Contato();
                            //ordenando 
                            contato.Nome = linhaComSplit[0];
                            contato.Email = linhaComSplit[1];
                            contato.NumeroTelefone = linhaComSplit[2];
                            contatosList.Add(contato); // adicionando o contato na lista
                        }
                    }
                }
            }
            return contatosList;
        }

        //Criando método para escrita no arquivo
        public static void EscreverArquivo(List<Contato> contatosList)
        {

            using (StreamWriter sw = new StreamWriter(@EnderecoArquivo, false)) //Escrever no arquivo 
            {
                foreach (Contato contato in contatosList)
                {
                    string linha = string.Format("{0};{1};{2}", contato.Nome, contato.Email, contato.NumeroTelefone);
                    sw.WriteLine(linha);
                }
                sw.Flush(); //Apaga os buffers
            } //Chama o Close() automaticamente.
        }

    }
}
