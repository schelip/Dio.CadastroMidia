using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Dio.CadastroMidia.Helpers
{
    public static class Drawing
    {
        /// <summary>
        /// Imprime imagem completa no console
        /// </summary>
		public static void ImprimeImagem(Image image)
		{
			FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);
			
			image.SelectActiveFrame(dimension, 0);

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
					GetNamedColor(cl);

					Console.Write(' ');
				}
				Console.ResetColor();
				Console.Write(" \n");
			}
            Console.WriteLine();
		}

        /// <summary>
        /// Imprime uma linha de uma imagem no console
        /// </summary>
        /// <param name="line">
        /// Índice da linha que será impressa
        /// </param>
        public static void ImprimeImagem(Image image, int line, int offset = 0)
        {
            FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);

			image.SelectActiveFrame(dimension, 0);

			if (image.Width > Console.WindowWidth - 2)
			{
				Console.WriteLine("Erro: A sua janela está pequena de mais para exibir essa imagem.");
				return;
			}

            Console.ResetColor();
            Console.Write(' ');
            for (int w = 0; w < image.Width; w++)
            {
                Color cl = ((Bitmap) image).GetPixel(w, line);
                Console.BackgroundColor = GetNamedColor(cl);

                Console.Write(' ');
            }
            Console.ResetColor();
            Console.Write(" \n");
        }

        /// <summary>
        /// Imprime uma secção de uma imagem no console
        /// </summary>
        /// <param name="lineI">
        /// Índice da primeira linha da secção
        /// </param>
        /// <param name="lineF">
        /// Índice da última linha da secção
        /// </param>
        public static void ImprimeImagem(Image image, int linhaI, int linhaF, int offset = 0)
        {
            FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);
			
			if (image.Width > Console.WindowWidth - 2)
			{
				Console.WriteLine("Erro: A sua janela está pequena de mais para exibir essa imagem.");
				return;
			}

			image.SelectActiveFrame(dimension, 0);

			for (int h = linhaI; h < linhaF; h++)
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
            int height = image.Height / ratio;

			Image thumbnail = bitmap.GetThumbnailImage(width, height, callback, IntPtr.Zero);
            
            return thumbnail;
        }

        // Útil //

        /// <summary>
        /// Muda a cor de fundo do console para a mais próxima possivel da recebida
        /// </summary>
        /// <example>
        /// <c>Color RGB(123, 80, 180)<c> resulta em <c>ConsoleColor.DarkMagenta = 5 (b0101)<c>
        /// </example>
		public static ConsoleColor GetNamedColor(Color color)
		{
			int claros = 0;
			StringBuilder str = new StringBuilder("0000");
			
			if (color.R > 85)
			{
				str[1] = '1';
				if (color.R > 170)
					claros++;
			}

			if (color.G > 85)
			{
				str[2] = '1';
				if (color.G > 170)
					claros++;
			}

			if (color.B > 85)
			{
				str[3] = '1';
				if (color.B > 170)
					claros++;
			}

			if (claros >= 2)
				str[0] = '1';

			int value = Convert.ToByte(str.ToString(), 2);
			return (ConsoleColor)value;
		}

        /// <summary>
        /// Método necessário para compilação de ToThumbnail
        /// </summary>
		private static bool ThumbnailCallback()
		{
			return false;
		}
    }
}