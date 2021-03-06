using System;
using System.Drawing;
using System.Collections;

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
			int cod =
				8 * Convert.ToByte(color.R > 170 || color.G > 170 || color.B > 170) +
				4 * Convert.ToByte(color.R > 85) +
				2 * Convert.ToByte(color.G > 85) +
				1 * Convert.ToByte(color.B > 85);
			
			return (ConsoleColor)cod;
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