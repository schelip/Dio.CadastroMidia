using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Dio.CadastroMidia.Helpers
{
    public static class Drawing
    {
		public static void TesteConsole()
		{
			foreach (ConsoleColor cl in System.Enum.GetValues(typeof(ConsoleColor)))
			{
				Console.BackgroundColor = cl;
				Console.Write(' ');
			}
			Console.ResetColor();
			Console.Write('\n');
		}

        /// <summary>
        /// Imprime imagem completa no console
        /// </summary>
		public static void ImprimeImagem(Image image)
		{
			if (image.Width > Console.WindowWidth - 2)
			{
				Console.WriteLine("Erro: A sua janela está pequena de mais para exibir essa imagem.");
				return;
			}

			Console.WriteLine();
			for (int h = 0; h < image.Height; h++)
			{
				Console.ResetColor();
				Console.Write(' ');
				for (int w = 0; w < image.Width; w++)
				{
					Color cl = ((Bitmap) image).GetPixel(w, h);
					Console.BackgroundColor = GetNamedColor(cl);

					Console.Write(' ');
				}
				Console.ResetColor();
				Console.Write(" \n");
			}
            Console.WriteLine();
		}

		/// <summary>
        /// Imprime imagem completa no console ao lado de um conjunto de strings
        /// </summary>
		public static void ImprimeImagem(Image image, string[] strings)
		{
			if (image.Width > Console.WindowWidth - 2)
			{
				Console.WriteLine("Erro: A sua janela está pequena de mais para exibir essa imagem.");
				return;
			}

			Console.WriteLine();
			for (int h = 0; h < image.Height; h++)
			{
				Console.ResetColor();
				Console.Write(' ');
				for (int w = 0; w < image.Width; w++)
				{
					Color cl = ((Bitmap) image).GetPixel(w, h);
					Console.BackgroundColor = GetNamedColor(cl);

					Console.Write(' ');
				}
				Console.ResetColor();
				
				if (h < strings.Length)
					Console.Write(" " + strings[h]);
				Console.Write(" \n");
			}
            Console.WriteLine();
		}

        /// <summary>
        /// Retorna a thumbnail de uma imagem
        /// </summary>
        /// <param name="width">
        /// Largura desejada para a thumbnail
        /// </param>
        public static Image ToThubmnail(Image image, int width)
        {
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
			Bitmap bitmap = new Bitmap(image);

			int ratio = image.Width / width;
            int height = image.Height / (ratio * 2); /* Multiplicar por 2 para evitar distorção
                                                        visto que o caractere unicode é um retangulo 1x2 */

			Image thumbnail = bitmap.GetThumbnailImage(width, height, callback, IntPtr.Zero);
            
            return thumbnail;
        }

        /// <summary>
        /// Muda a cor de fundo do console para a mais próxima possivel da recebida
        /// </summary>
        /// <example>
        /// <c>Color RGB(123, 80, 180)<c> resulta em <c>ConsoleColor.DarkMagenta = 5 (b0101)<c>
        /// </example>
		public static ConsoleColor GetNamedColor(Color color)
		{
			StringBuilder str = new StringBuilder("0000");
			
			if (color.R > 85)
			{
				str[1] = '1';
				if (color.R > 170)
					str[0] = '1';
			}

			if (color.G > 85)
			{
				str[2] = '1';
				if (color.G > 170)
					str[0] = '1';
			}

			if (color.B > 85)
			{
				str[3] = '1';
				if (color.B > 170)
					str[0] = '1';
			}

			int value = Convert.ToByte(str.ToString(), 2);
			return (ConsoleColor)value;
		}

		/// Útil

        /// <summary>
        /// Método necessário para compilação de ToThumbnail
        /// </summary>
		private static bool ThumbnailCallback()
		{
			return false;
		}
    }
}