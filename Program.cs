using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace CSVConsoleSisap
{
    class Program
    {
        public static string DELIMITADOR = ";";
        public static string csvImportacao = "p.csv";
        public static string arquivoSaida = "saida.csv";
        public static string arquivoTeste = "teste.csv";
        public static string arqNomesMasc = "ibge-masculino.csv";
        public static string arqNomesFemi = "ibge-feminino.csv";
        public static ArquivoCSV nomesM = new ArquivoCSV(arqNomesMasc);   //Arquivo do IBGE com nomes 10000 Masculinos mais frequentes
        public static ArquivoCSV nomesF = new ArquivoCSV(arqNomesFemi);   //Arquivo do IBGE com nomes 10000 Femininos mais frequentes
        public static Encoding encoding = Encoding.UTF8;
        public static readonly Regex regexNumero = new Regex(@"^\d+$"); //Regex pré-compilada para testar strings numéricas
                                                                        //Array gigante que vai receber os dados do CSV
        public static readonly string[] headerPadrao = new string[]     //Cabeçalho com todos os campos possíveis de importação
        {
            "Nome",                         //Index: 0
            "Sexo",                         //Index: 1
            "Nome_Destinatario",            //Index: 2
            "Tratamento",                   //Index: 3
            "Data_Nascimento",              //Index: 4
            "Logradouro_1",                 //Index: 5
            "Bairro_1",                     //Index: 6
            "Localidade_1",                 //Index: 7
            "Cep_1",                        //Index: 8
            "Logradouro_2",                 //Index: 9
            "Bairro_2",                     //Index: 10
            "Localidade_2",                 //Index: 11
            "Cep_2",                        //Index: 12
            "Logradouro_3",                 //Index: 13
            "Bairro_3",                     //Index: 14
            "Localidade_3",                 //Index: 15
            "Cep_3",                        //Index: 16
            "Logradouro_4",                 //Index: 17
            "Bairro_4",                     //Index: 18
            "Localidade_4",                 //Index: 19
            "Cep_4",                        //Index: 20
            "Logradouro_5",                 //Index: 21
            "Bairro_5",                     //Index: 22
            "Localidade_5",                 //Index: 23
            "Cep_5",                        //Index: 24
            "Tipo_Telefone_1",              //Index: 25
            "Valor_Telefone_1",             //Index: 26
            "Observacao_Telefone_1",        //Index: 27
            "Tipo_Telefone_2",              //Index: 28
            "Valor_Telefone_2",             //Index: 29
            "Observacao_Telefone_2",        //Index: 30
            "Tipo_Telefone_3",              //Index: 31
            "Valor_Telefone_3",             //Index: 32
            "Observacao_Telefone_3",        //Index: 33
            "Tipo_Telefone_4",              //Index: 34
            "Valor_Telefone_4",             //Index: 35
            "Observacao_Telefone_4",        //Index: 36
            "Tipo_Telefone_5",              //Index: 37
            "Valor_Telefone_5",             //Index: 38
            "Observacao_Telefone_5",        //Index: 39
            "Tipo_Telefone_6",              //Index: 40
            "Valor_Telefone_6",             //Index: 41
            "Observacao_Telefone_6",        //Index: 42
            "Tipo_Telefone_7",              //Index: 43
            "Valor_Telefone_7",             //Index: 44
            "Observacao_Telefone_7",        //Index: 45
            "Tipo_Telefone_8",              //Index: 46
            "Valor_Telefone_8",             //Index: 47
            "Observacao_Telefone_8",        //Index: 48
            "Tipo_Telefone_9",              //Index: 49
            "Valor_Telefone_9",             //Index: 50
            "Observacao_Telefone_9",        //Index: 51
            "Tipo_Endereco_Eletronico_1",   //Index: 52
            "Valor_Endereco_Eletronico_1",  //Index: 53
            "Tipo_Endereco_Eletronico_2",   //Index: 54
            "Valor_Endereco_Eletronico_2",  //Index: 55
            "Tipo_Endereco_Eletronico_3",   //Index: 56
            "Valor_Endereco_Eletronico_3",  //Index: 57
            "Tipo_Endereco_Eletronico_4",   //Index: 58
            "Valor_Endereco_Eletronico_4",  //Index: 59
            "Tipo_Endereco_Eletronico_5",   //Index: 60
            "Valor_Endereco_Eletronico_5",  //Index: 61
            "Tipo_Endereco_Eletronico_6",   //Index: 62
            "Valor_Endereco_Eletronico_6",  //Index: 63
            "Tipo_Endereco_Eletronico_7",   //Index: 64
            "Valor_Endereco_Eletronico_7",  //Index: 65
            "Tipo_Endereco_Eletronico_8",   //Index: 66
            "Valor_Endereco_Eletronico_8",  //Index: 67
            "Tipo_Endereco_Eletronico_9",   //Index: 68
            "Valor_Endereco_Eletronico_9",  //Index: 69
            "Grupo_1",                      //Index: 70
            "Grupo_2",                      //Index: 71
            "Grupo_3",                      //Index: 72
            "Grupo_4",                      //Index: 73
            "Grupo_5",                      //Index: 74
            "Grupo_6",                      //Index: 75
            "Grupo_7",                      //Index: 76
            "Grupo_8",                      //Index: 77
            "Grupo_9",                      //Index: 78
            "Partido_Politico",             //Index: 79
            "Profissao",                    //Index: 80
            "Observacao",                   //Index: 81
            "Cargo_1",                      //Index: 82
            "Cargo_2",                      //Index: 83
            "Cargo_3",                      //Index: 84
            "Cargo_4",                      //Index: 85
            "Cargo_5",                      //Index: 86
            "Cargo_6",                      //Index: 87
            "Cargo_7",                      //Index: 88
            "Cargo_8",                      //Index: 89
            "Cargo_9",                      //Index: 90
            "Dia",                          //Index: 91 - Para nascimento com data separada
            "Mes",                          //Index: 92 - Para nascimento com data separada
            "Ano",                          //Index: 93 - Para nascimento com data separada
            "ERROS"                         //Index: 94 - Erros encontrados
        }; //Cabeçalho padrão com todos campos disponíveis possíveis

        public static int TotalDeLinhasArquivo = 0;
        public class Telefone {
            public Telefone(string num) //Construtor vai receber uma chamada do tipo "Telefone ttt = new Telefone("31973605009");
            {
                this.Numero = num;
                validaTel();

                Console.Write($"Tel: {num} - ");
                Console.Write($"Formatado: {Numero} - ");
                Console.Write($"Tipo: {Tipo} - ");
                Console.WriteLine($"Erro: {Erro}");
            }
            public string Numero { get; set; }
            public string Tipo { get; set; }
            public string Observacao { get; set; }
            public string Erro { get; set; }

            public void validaTel()
            {

                Regex NonDigits = new Regex(@"[^\d]+"); // usar para remover os caracteres e deixar apenas dígitos
                Regex NonDigitsTel = new Regex(@"[^\d\(\)\-\s]+"); // usar para verificar se há caracteres diferentes de dígitos, (, ) e -
                Regex cel_pattern = new Regex(@"^([0]?[1-9]{2})?(9\d{4})(\d{4})$"); //padrão para celular
                Regex cel_8dig_pattern = new Regex(@"^([0]?[1-9]{2})?([7-9]\d{3})(\d{4})$");
                Regex fixo_pattern = new Regex(@"^([0]?\d{2})?([2-7]\d{3})(\d{4})$"); //padrão para fixo

                string tel = this.Numero;
                string tel_adjust = "";

                //verifica se há letras ou outros caracteres diferentes de espaço, (, ) e -
                Match m = NonDigitsTel.Match(tel);

                if (m.Success) //encontrou caracteres diferentes do esperado para telefones
                {
                    this.Erro = "Telefone inválido - Caracter não permitido";
                    return;
                }
                else //contem apenas caracteres válidos. verifica se o número é válido e determina o tipo (celular ou fixo)
                {
                    string tel_cleaned = NonDigits.Replace(tel, ""); //mantém apenas os números para uniformizar o formato

                    Match tel_celular = cel_pattern.Match(tel_cleaned);
                    if (tel_celular.Success) // é um celular. formatar corretamente e marcar o tipo
                    {
                        tel_adjust = tel_celular.Groups[2] + "-" + tel_celular.Groups[3];
                        if (!string.IsNullOrEmpty(tel_celular.Groups[1].Value))
                        {
                            tel_adjust = "(" + tel_celular.Groups[1] + ")" + tel_adjust;
                        }
                        this.Numero = tel_adjust;
                        this.Tipo = "CE";
                        return;
                    }
                    else
                    {
                        tel_celular = cel_8dig_pattern.Match(tel_cleaned);
                        if (tel_celular.Success) // é um celular com formato antigo (8 digitos). formatar corretamente e marcar o tipo
                        {
                            tel_adjust = tel_celular.Groups[2] + "-" + tel_celular.Groups[3];
                            if (!string.IsNullOrEmpty(tel_celular.Groups[1].Value))
                            {
                                tel_adjust = "(" + tel_celular.Groups[1] + ")" + tel_adjust;
                            }
                            this.Numero = tel_adjust;
                            this.Tipo = "CE";
                            return;
                        }
                        else
                        {
                            Match tel_fixo = fixo_pattern.Match(tel_cleaned);
                            if (tel_fixo.Success) // é um fixo. formatar corretamente e marcar o tipo
                            {
                                tel_adjust = tel_fixo.Groups[2] + "-" + tel_fixo.Groups[3];
                                if (!string.IsNullOrEmpty(tel_fixo.Groups[1].Value))
                                {
                                    tel_adjust = "(" + tel_fixo.Groups[1] + ")" + tel_adjust;
                                }
                                this.Numero = tel_adjust;
                                this.Tipo = "FR";
                                return;
                            }
                            else
                            {
                                this.Erro = "Telefone inválido - Padrão incorreto";
                                return;
                            }
                        }
                    }
                }
            }

        }
        public class Nome
        {
            public Nome(string nome) 
            {
                this.nome = nome;
                this.primeiroNome = separaPrimeiroNome(this.nome);
                FreqM = getFrequencia(primeiroNome, nomesM);
                FreqF = getFrequencia(primeiroNome, nomesF);
                //Console.WriteLine($"{primeiroNome} - M:{FreqM} - F:{FreqF}");
            
            }

            public string nome { get; set; }
            public string primeiroNome { get; set; }
            public Boolean M { get; set; } //sexo masculino
            public Boolean F { get; set; } //sexo feminino
            public float FreqM { get; set; }
            public float FreqF { get; set; }
            public float getFrequencia(string nom, ArquivoCSV tabelaIBGE) { //Pega a frequência em q o nome aparece no arquivo do IBGE correspondente ao sexo


                //Console.WriteLine($"{nom}");
                int linha = Array.IndexOf(tabelaIBGE.coluna(0), nom);
                if (linha == -1)
                {
                    //Console.WriteLine($"{nom} - Nome não encontrado no IBGE");
                    return 0.01F;
                }
                else {
                    string s = tabelaIBGE.coluna(2)[linha]; //Pega a frequência do nome na pesquisa do IBGE
                    return (float) Int32.Parse(s);
                }
            }
            public string separaPrimeiroNome(string n) //E TIRA ACENTOS
            {
                int espaco = n.IndexOf(" ");
                bool retirarAcentos = true; //desligar para passar os nomes acentuados (IBGE não contém acentos)
                string str = (espaco == -1 ? n : n.Substring(0,espaco)); //Se espaço == -1, não foi encontrado caractere de espaço, e retorna o nome completo. Senão retorna do primero caractere até o 1o espaço.
                if (retirarAcentos)
                {
                    string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
                    string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
                    for (int i = 0; i < acentos.Length; i++)
                    {
                        str = str.Replace(acentos[i], semAcento[i]);
                    }

                }

                return str;

            }
            public string getSexo() //Retorna 'M'asculino, 'F'eminino ou 'D'esconhecido
            {
                float margemAcerto = 20; //20 vezes mais ocorrências entre um sexo e outro nas frequências do IBGE
                string sexo="D";
                if ((FreqM/FreqF) > margemAcerto )
                {
                    //Console.WriteLine($"Male ratio: {FreqM}/{FreqF} = {FreqM / FreqF} ");
                    sexo = "M";

                }
                else if((FreqF / FreqM) > margemAcerto)
                {
                   // Console.WriteLine($"Feme ratio:{FreqF}/{FreqM} = {FreqF / FreqM} ");
                    sexo = "F";
                }
                //Console.WriteLine($"DEFINIDO SEXO: {sexo}");
                //Console.WriteLine("-.-.-.-.-.-");
                return sexo;
            }
        }
        public class ArrayPadrao {
            public ArrayPadrao(string[] header, ArquivoCSV arquivo)
            {
                this.header = header;
                this.numLinhas = arquivo.numLinhas;
                this.numColunas = header.Length;
                this.dados = new string[numLinhas, numColunas]; //Tamanho do Array Padrão = [Linhas do arquivo CSV x Colunas do header padrão]
                transfereHeader();
                this.indexTransferencia = identificaDados(arquivo.header); //lê o cabeçalho do CSV e identifica com base no header Padrão.
                transfereDados(arquivo.dados); // transfere dados do CSV pras respectivas colunas do header Padrão.
            }

            public string[,] dados { get; set; } //Dados completos do Array, incluindo cabeçalho
            public string[] header { get; set; } //Apenas o cabeçalho padrão
            public int numLinhas { get; set; } //Número total de linhas (Total de linhas do CSV + 1 do cabeçalho)
            public int numColunas { get; set; }//Número total de colunas 

            public int[] indexTransferencia { get; set; } //Índice para transferir do CSV para a coluna correta do Array Padrão
            public int[] identificaDados(string[] cabecalho)
            {
                Linha();
                List<int> indexCampos = new List<int>();
                Console.WriteLine("COLUNAS ENCONTRADAS:");
                foreach (string s in cabecalho)
                {
                    //indexCampos.Add(Array.IndexOf(header, s));
                    int indice = Array.IndexOf(header, s);
                    indexCampos.Add(indice);

                    if (indice == -1)
                    {
                        Console.WriteLine($"CAMPO {s} NÃO ENCONTRADO! NOME DE CAMPO INCORRETO."); //ERRO DE CABEÇALHO INCORRETO
                    }
                    else
                    {
                        Console.WriteLine($"  {indice}  =  {s}");
                    }
                }
                return indexCampos.ToArray();
            } //Identifica os campos do CSV dentre os campos padrão. Coloca -1 nos não encontrados.
            public void transfereHeader() { //Copia cabeçalho para a primeira linha do array 'dados'
                for (int i = 0; i < numColunas; i++)
                {
                    dados[0, i] = header[i];
                    //Console.WriteLine($"COLUNA {i} = dados[0,i]={dados[0, i]} - header[i]={header[i]} ");
                    ; }
            }
            private void transfereDados(string[,] dadosCSV) //transfere os dados do CSV para as respectivas colunas do array padrão
            {
                for (int i = 1; i < numLinhas; i++) // Já que i=0 é o cabeçalho, já preenchido, este for é i=1 , a partir da segunda linha
                {
                    for (int j = 0; j < indexTransferencia.Length; j++)
                    {
                        this.dados[i, indexTransferencia[j]] = dadosCSV[i, j];
                    }
                }
            }
            public int indexDe(string s) { //retorna o índice da coluna, baseado no nome
                int x = Array.IndexOf(header, s);
                if (x == -1)
                {
                    Console.WriteLine($"CAMPO {s} NÃO FOI ENCONTRADO");
                }
                return x;
            }
            public string[] coluna(int num)
            { //Recebe string[,] e retorna string[] com a primeira coluna.
                List<string> Lista = new List<string>();
                for (int i = 0; i < dados.GetLength(0); i++)
                {
                    Lista.Add(dados[i, num]);
                }
                return Lista.ToArray();
            }
            public void validaSexo() {
                var cronometro = Stopwatch.StartNew();
                int iSexo = indexDe("Sexo");     //número-índice da coluna em que os sexos estão
                int iNome = indexDe("Nome");    //número-índice da coluna em que os nomes estão
                string s;
                if (iSexo == -1 ^ iNome == -1)
                {
                    Console.WriteLine("Validação de sexo não será executada: CAMPO NÃO ENCONTRADO");
                }
                else {
                    Linha();
                    Console.WriteLine("Validando campo sexo");
                    for (int i = 1; i < numLinhas; i++)
                    {
                        
                        Nome n = new Nome(dados[i, iNome]);
                        s = n.getSexo();
                        int printar = i % 1000;
                            if (printar == 0) {
                            Console.Write($".");
                            n = null;
                        }
                        
                        dados[i, iSexo] = s; //escreve na coluna sexo do Array. SUBSTITUIR POR BLOCO TRY CATCH
                    }
                    cronometro.Stop(); // para cronometro
                    Linha();
                    Console.WriteLine($"\nRotina de validação de sexo concluída. TEMPO GASTO: {cronometro.Elapsed.ToString()}");
                }
            }

            public void classificaTelefones() //Testa todos os números de telefone, formata e insere o tipo
            {
                var cronometroTel = Stopwatch.StartNew();
                //int[] Tel1 = { indexDe("Valor_Telefone_1"), indexDe("Tipo_Telefone_1"), indexDe("ERROS") };
                //int[] Tel2 = { indexDe("Valor_Telefone_2"), indexDe("Tipo_Telefone_2"), indexDe("ERROS") };
                //int[] Tel3 = { indexDe("Valor_Telefone_3"), indexDe("Tipo_Telefone_3"), indexDe("ERROS") };
                //int[] Tel4 = { indexDe("Valor_Telefone_4"), indexDe("Tipo_Telefone_4"), indexDe("ERROS") };
                //int[] Tel5 = { indexDe("Valor_Telefone_5"), indexDe("Tipo_Telefone_5"), indexDe("ERROS") };
                //int[] Tel6 = { indexDe("Valor_Telefone_6"), indexDe("Tipo_Telefone_6"), indexDe("ERROS") };
                //int[] Tel7 = { indexDe("Valor_Telefone_7"), indexDe("Tipo_Telefone_7"), indexDe("ERROS") };
                //int[] Tel8 = { indexDe("Valor_Telefone_8"), indexDe("Tipo_Telefone_8"), indexDe("ERROS") };
                //int[] Tel9 = { indexDe("Valor_Telefone_9"), indexDe("Tipo_Telefone_9"), indexDe("ERROS") };

                //int itel = indexDe("Sexo");     //número-índice da coluna em que os sexos estão
                //int itipo = indexDe("Nome");    //número-índice da coluna em que os nomes estão
                //string s;
                //if (itel == -1)
                //{
                //    Console.WriteLine("Validação de telefones não será executada: CAMPO NÃO ENCONTRADO");
                //}
                //else
                //{
                //    Linha();
                //    Console.Write("Validando Telefones");
 
                //}
                    cronometroTel.Stop(); // para cronometro
                    Linha();
                    Console.WriteLine($"\nRotina de validação de telefones concluída. TEMPO GASTO: {cronometroTel.Elapsed.ToString()}");
            }
           

                public string[,] Finalizado() { //retorna Array contendo apenas os campos que foram utilizados, descartando colunas vazias.
                List<int> utilizados = new List<int>();
                for (int i = 0; i < numColunas; i++)
                {
                    int contadorNaoVazios = 0;
                    for (int j = 1; j < numLinhas; j++)
                    {
                        if (!String.IsNullOrEmpty(dados[j, i])){
                            contadorNaoVazios++;
                        }
                    }
                    if (contadorNaoVazios >0)
                    {
                        utilizados.Add(i); //Se existe algum valor, o índice desta coluna é incluído na lista.
                    }
                    Console.WriteLine($"{contadorNaoVazios} valores - NA COLUNA: {header[i]}");
                }


                string[,] arrayFinal = new string[numLinhas, utilizados.Count];
                for (int i = 0; i < numLinhas; i++)
                {
                    for (int j = 0; j < utilizados.Count; j++)
                    {
                        arrayFinal[i, j] = dados[i, utilizados[j]];
                    }
                }
                return arrayFinal;
            }
        }      
        public class ArquivoCSV
        {
            public ArquivoCSV(string nome) //Recebe nome do arquivo e carrega os dados para array em objeto.dados
            {
                this.nome = nome;
                Console.Write($"* * ARQUIVO: {this.nome}: ");
                this.dados = this.csvPraArray();
  

                Console.WriteLine($"CARREGADO. ({this.numLinhas} linhas, {this.numColunas} colunas)");
            }
            public string[] header { get; set; }


            public string nome { get; set; }
            public int numLinhas { get; set; }
            public int numColunas { get; set; }
            public string[,] dados { get; set; }
            public string[,] csvPraArray()
            {
                using (TextFieldParser parser = new TextFieldParser(this.nome))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(DELIMITADOR);      //configura o delimitador (padrão: ponto-e-virgula)
                    parser.HasFieldsEnclosedInQuotes = true; //Todos os campos de texto com aspas
                    this.numLinhas = File.ReadLines(this.nome).Count();
                    this.header = parser.ReadFields();
                    this.numColunas = this.header.Length;

                    string[,] temp = new string[this.numLinhas, this.numColunas];
                    for (int k = 0; k < this.header.Length; k++)   //COPIA O CABEÇALHO pra PRIMEIRA LINHA DO ARRAY
                    {
                        temp[0, k] = this.header[k];
                    }

                    for (int j = 1; !parser.EndOfData; j++) //Copia os demais dados pro array
                    {
                        string[] fields = parser.ReadFields();
                        for (int i = 0; i < fields.Length; i++)
                        {
                            temp[j, i] = fields[i];
                        }
                    }
                    return temp;
                }
            }
            public string[] getPrimeiraColuna(string[,] a) { //Recebe string[,] e retorna string[] com a primeira coluna.
                List<string> Lista = new List<string>();
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    Lista.Add(a[i, 0]);
                }
                return Lista.ToArray();
            }
            public string[] coluna(int num)
            { //Recebe string[,] e retorna string[] com a primeira coluna.
                List<string> Lista = new List<string>();
                for (int i = 0; i < dados.GetLength(0); i++)
                {
                    Lista.Add(dados[i, num]);
                }
                return Lista.ToArray();
            }
        }


        static void Main(string[] args)
        {
            Console.WriteLine("INICIO");
            Linha();



            ArquivoCSV dadosImportacao = new ArquivoCSV(csvImportacao);
            ArrayPadrao arrayPadrao = new ArrayPadrao(headerPadrao, dadosImportacao); //Transfere os dados do CSV para o array com todos os campos possíveis

            arrayPadrao.validaSexo();

            SalvaArrayPraArquivo(arquivoTeste, arrayPadrao.Finalizado());

            string[] testeTels = new string[] {
                "(38) 99482427",
                "(38) 998752466",
                "(38) 991504698",
                "(38) 99066615",
                "(38) 91237721",
                "(38) 99173394",
                "(38) 97459532",
                "(38) 99079338",
                "(38) 99066615",
                "(38) 99372419",
                "(038) 998591692",
                "(038) 99151443",
                "(038) 99982382",
                "(038) 32163016",
                "(038) 92394557",
                "(038) 91821034",
                "(038) 99982793",
                "(038) 99261500",
                "(038) 98081331",
                "(038) 91516315",
                "38 91332934",
                "38 98081331",
                "38 91215657",
                "38 99560343",
                "38 998309231",
                "38 992128858",
                "38 91147614",
                "38 99560343",
                "38 98421235",
                "38 998671134",
                "(38) 89313391",
                "(38) 81456699",
                "(38) 81178001",
                "(38) 89993167",
                "(38) 89417015",
                "(38) 79442660",
                "(38) 78018084",
                "(38) 79311977",
                "(38) 9991323890",
                "(38) 78897613",
                "(38) 21990088",
                "(38) 31733375",
                "(38) 29637348",
                "(38) 41973712",
                "(38) 291747854",
                "(38) 29582408",
                "(038) 29604449",
                "(38) 31216628",
                "(038) 31113198",
                "27357511",
                "38732263",
                "(38) 999097317",
                "(38) 998390692",
                "(38) 91156889",
                "(38) 991784982",
                "(38) 998556303",
                "(38) 91176610",
                "(38) 92121583",
                "(38) 91850584",
                "(38) 91403844",
                "(38) 997497285",
                "(38) 992362143",
                "(38) 999509604",
                "(38) 991903782",
                "(38) 998576673",
                "(38) 99984310",
                "(38) 998390444",
                "(38) 991528497",
                "(38) 99545005",
                "(38) 999126834",
                "(38) 84130303",
                "(38) 999550648",
                "(38) 91739262",
                "(38) 999328091",
                "(38) 99550648",
                "(38) 98996037",
                "(38) 91215767",
                "(38) 999936088",
                "(38) 84213208",
                "(38) 98331190",
                "(38) 99246127",
                "(38) 998927642",
                "(38) 92230430",
                "(38) 99251961",
                "(38) 91852920",
                "(38) 91893244",
                "(38) 98189414",
                "(38) 999896200",
                "(38) 98318740",
                "(38) 991456699",
                "(38) 91391927",
                "(38) 998191913",
                "(38) 91154587",
                "(38) 91955041",
                "(38) 91979265",
                "(38) 91154587",
                "(38) 98986315",
                "(38) 91902722",
                "(38) 99197127",
                "(38) 999896200",
                "(38) 98589748",
                "(38) 98277311",
                "(38) 99953114",
                "(31) 98015132",
                "(38) 84139041",
                "(38) 991914891",
                "(38) 99146208",
                "(38) 99304425",
                "(38) 92280784",
                "(38) 91261110",
                "(38) 91289623",
                "(38) 999291437",
                "(38) 999297359",
                "(38) 99696800",
                "(38) 98131512",
                "(38) 997303233",
                "(38) 997446188",
                "(38) 99158331",
                "(38) 99215861",
                "(38) 998149105",
                "(38) 99380217",
                "(38) 992266652",
                "(38) 99798796",
                "(38) 99955720",
                "(38) 98598724",
                "(38) 99158331",
                "(38) 991902722",
                "(38) 91324695",
                "(38) 99246107",
                "(38) 998208569",
                "(38) 984173791",
                "(38) 991562550",
                "(38) 99040907",
                "(31) 996159887",
                "(38) 99936131",
                "(38) 91932624",
                "(38) 99001850",
                "(38) 999781081",
                "(38) 91601799",
                "(38) 999291437",
                "(38) 98495643",
                "(38) 999841538",
                "(38) 99580557",
                "(38) 999166403",
                "(38) 91231162",
                "(38) 92060371",
                "(38) 991638318",
                "(38) 99283872",
                "(38) 991336241",
                "(38) 92143280",
                "(38) 99595660",
                "(38) 91714408",
                "(38) 99733104",
                "(38) 91179704",
                "(38) 92197325",
                "(38) 91261211",
                "(38) 999509574",
                "(38) 88054424",
                "(38) 991237517",
                "(38) 98387440",
                "(38) 998584253",
                "(38) 99076048",
                "(38) 91694888",
                "(38) 98134646",
                "(38) 999612172",
                "(38) 99916311",
                "(38) 999703165",
                "(38) 91700753",
                "(38) 998496509",
                "(33) 91171345",
                "(33) 91065245",
                "(38) 99882493",
                "(38) 91948004",
                "(38) 99953114",
                "(38) 999648656",
                "(38) 999891368",
                "(38) 98890880",
                "(38) 999261500",
                "(38) 991994892",
                "(38) 999669435",
                "(38) 98180434",
                "(38) 88194649",
                "(38) 988194649",
                "(38) 991230470",
                "(38) 91918880",
                "(38) 99172320",
                "(38) 91173872",
                "(38) 91858316",
                "(38) 991122021",
                "(38) 99186715",
                "(38) 999784050",
                "(38) 92471302",
                "(38) 98196647",
                "(38) 99494378",
                "(38) 999297952",
                "(38) 999982382",
                "(38) 91406251",
                "(38) 97508020",
                "(38) 999983720",
                "(38) 92433654",
                "(38) 999291457",
                "(38) 98191161",
                "(38) 999757475",
                "(38) 999105469",
                "(38) 98667527",
                "(38) 99826644",
                "(38) 91567685",
                "(38) 991296563",
                "(38) 99297617",
                "(38) 99497298",
                "(38) 991100302",
                "(38) 98075463",
                "(38) 991689577",
                "(38) 998208569",
                "(38) 998715354",
                "(38) 91415907",
                "(11) 986046616",
                "(38) 999289007",
                "(38) 99049814",
                "(38) 999692520",
                "(38) 99300752",
                "(38) 97401439",
                "(38) 998083519",
                "(38) 999142643",
                "(38) 99165557",
                "(38) 98118374",
                "(38) 98908270",
                "(38) 998068803",
                "(38) 999789740",
                "(38) 998188206",
                "(38) 998286744",
                "(38) 98728909",
                "(38) 999405872",
                "(38) 997425244",
                "(38) 99659822",
                "(38) 99739909",
                "(38) 97318115",
                "(38) 99494964",
                "(38) 98096948",
                "(33) 999462633",
                "(38) 99623588",
                "(38) 91427577",
                "(38) 91673350",
                "(38) 84079513",
                "(38) 99327273",
                "(38) 997503073",
                "(38) 91882457",
                "(38) 88222494",
                "(38) 91436035",
                "(38) 91909196",
                "(38) 97290888",
                "(38) 91625343",
                "(38) 99660727",
                "(38) 998387543",
                "(38) 99399759",
                "(38) 91328766",
                "(38) 92171499",
                "(38) 99071190",
                "(38) 99479555",
                "(38) 98697938",
                "(38) 322235769",
                "(38) 91959640",
                "(38) 99061950",
                "(38) 99905971",
                "(38) 99786300",
                "(38) 99931900",
                "(38) 99905971",
                "(38) 91001907",
                "(38) 997341626",
                "(38) 84118613",
                "(38) 992193867",
                "(38) 999273084",
                "(38) 91691884",
                "(38) 91099654",
                "(38) 999271488",
                "(38) 97385150",
                "(38) 99270072",
                "(38) 998515324",
                "(38) 99118257",
                "(38) 992254563",
                "(38) 91660283",
                "(38) 991120162",
                "(38) 91837324",
                "(38) 991660283",
                "(38) 91259305",
                "(38) 991492362",
                "(38) 91344663",
                "(38) 99056681",
                "(38) 99872342",
                "(38) 91467769",
                "(38) 91467769",
                "(38) 91623193",
                "(38) 88030330",
                "(38) 91215169",
                "(38) 999799440",
                "(38) 99523048",
                "(38) 92370042",
                "(38) 999068013",
                "(38) 991717857",
                "(38) 988398913",
                "(38) 99029234",
                "(38) 98515324",
                "(38) 91579662",
                "(38) 32231567",
                "(38) 92326193",
                "(38) 91289948",
                "(38) 991458519",
                "(38) 992326193",
                "(38) 998326193",
                "(38) 98967136",
                "(38) 91186123",
                "(38) 997238201",
                "(38) 98240393",
                "(38) 99473798",
                "(38) 91922185",
                "(38) 92173416",
                "(38) 91314041",
                "(38) 321019695",
                "(38) 91761149",
                "(38) 99929603",
                "(38) 98528075",
                "(38) 84159683",
                "(38) 991114603",
                "(38) 99988642",
                "(38) 88320271",
                "(38) 988314544",
                "(38) 98483201",
                "(38) 991408247",
                "(38) 99079525",
                "(38) 91591231",
                "(38) 91340623",
                "(38) 91029231",
                "(38) 9729236"
            };
            //foreach(string s in testeTels){
            //    Telefone t = new Telefone(s);
            //    t = null;
            //}

            Console.WriteLine("Pressione ENTER para sair...");
            Console.ReadLine();
        }

        static string FormataAspas(string S)
        { //Se não for uma string puramente numérica, coloca entre aspas.

            if (regexNumero.IsMatch(S)){
                return S;
            }
            else
            {
                return "\"" + S + "\"";
            }
        }
        public static Encoding DetectBOMBytes(byte[] BOMBytes) //Reconhece a codificação de um arquivo. Ainda não utilizado.
        {
            if (BOMBytes == null)
                throw new ArgumentNullException("Must provide a valid BOM byte array!", "BOMBytes");

            if (BOMBytes.Length < 2)
                return null;

            if (BOMBytes[0] == 0xff
                && BOMBytes[1] == 0xfe
                && (BOMBytes.Length < 4
                    || BOMBytes[2] != 0
                    || BOMBytes[3] != 0
                    )
                )
                return Encoding.Unicode;

            if (BOMBytes[0] == 0xfe
                && BOMBytes[1] == 0xff
                )
                return Encoding.BigEndianUnicode;

            if (BOMBytes.Length < 3)
                return null;

            if (BOMBytes[0] == 0xef && BOMBytes[1] == 0xbb && BOMBytes[2] == 0xbf)
                return Encoding.UTF8;

            if (BOMBytes[0] == 0x2b && BOMBytes[1] == 0x2f && BOMBytes[2] == 0x76)
                return Encoding.UTF7;

            if (BOMBytes.Length < 4)
                return null;

            if (BOMBytes[0] == 0xff && BOMBytes[1] == 0xfe && BOMBytes[2] == 0 && BOMBytes[3] == 0)
                return Encoding.UTF32;

            if (BOMBytes[0] == 0 && BOMBytes[1] == 0 && BOMBytes[2] == 0xfe && BOMBytes[3] == 0xff)
                return Encoding.GetEncoding(12001);

            return null;

            //byte[] bomBytes = new byte[InputFileStream.Length > 4 ? 4 : InputFileStream.Length];
            //InputFileStream.Read(bomBytes, 0, bomBytes.Length);

            //encodingFound = DetectBOMBytes(bomBytes);
        }
       
        public static void SalvaArrayPraArquivo(string nomeArquivo, string[,] dados) {
            var cron = Stopwatch.StartNew(); //inicia cronômetro
            using (var fs = new FileStream(nomeArquivo, FileMode.Create)) //FAZER CATCH DE ARQUIVO INEXISTENTE OU SENDO USADO
            using (var sw = new StreamWriter(fs))
            {
                for (int i = 0; i < dados.GetLength(0); i++)
                {
                    for (int j = 0; j < dados.GetLength(1); j++)
                    {
                        if (String.IsNullOrEmpty(dados[i, j])){
                            sw.Write(DELIMITADOR);
                        }
                        else {
                        sw.Write(FormataAspas(dados[i,j]) + DELIMITADOR);
                        }
                    }
                    sw.Write("\r\n"); //CR LF - nova linha
                }
            }
            cron.Stop(); // para cronometro
            Linha();
            Console.WriteLine($"Arquivo de saída: {nomeArquivo} ESCRITO. TEMPO GASTO: {cron.Elapsed.ToString()}");
        }
        public static void IgnorarTestedeLista()
        {
            var tst = new List<int>();
            tst.Add(1);
            tst.Add(3);
            tst.Add(5);
            tst.Insert(1, 2);

            foreach (int i in tst)
            {
                Console.WriteLine(i);
            }
        }
        public static void Linha()
        {
            Console.WriteLine(" = = = = = = = = = = = = = = = = = =");
        }
    }
}
