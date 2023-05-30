using System.IO;
using System.Text;

namespace AntoFileIO
{
	enum FileModeIO
	{
		Read,
		Write,
		All
	}
	
	class FileIO
	{
		private string path = "";
		private FileModeIO mode;
		private StreamWriter? sw;
		private StreamReader? sr;
		
		public FileIO() { }
		
		public FileIO(string path)
		{
			this.path = path;
		}
		
		public FileIO(string path, FileModeIO mode)
		{
			this.path = path;
			this.mode = mode;
		}
		
		public void Append(string text, bool newLine)
		{
			if (this.mode == FileModeIO.Read) return;
			
			using (sw = new StreamWriter(path, true))
				sw.Write((newLine ? "\n" : "") + text);
		}
		
		public void Write(string text)
		{
			if (this.mode == FileModeIO.Read) return;
			
			using (sw = new StreamWriter(path, false))
				sw.Write(text);
		}
		
		public string GetContent()
		{
			if (this.mode != (FileModeIO.Read | FileModeIO.All)) return "";
			
			using (sr = new StreamReader(path))
				return sr.ReadToEnd();
		}
		
		public string GetContent(int line)
		{
			if (this.mode == FileModeIO.Write) return "";
			
			using (sr = new StreamReader(path))
			{
				string? s = "";
				int i = 0;
				
				do
				{
					s = sr.ReadLine();
					if (i == line) return s != null ? s : "";
					i++;
				}
				while (s != null);
			}
			return "";
		}
		
		public void Set(string path)
		{
			this.path = path;
		}
		
		public void Set(FileModeIO mode)
		{
			this.mode = mode;
		}
		
		public void Set(string path, FileModeIO mode)
		{
			this.path = path;
			this.mode = mode;
		}
		
		public void Clear()
		{
			if (this.mode == FileModeIO.Read) return;
			
			using (StreamWriter sw = new StreamWriter(path, false))
				sw.Write("");
		}
	}
}
