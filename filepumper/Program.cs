using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace filepumper {
    class Program {
        static void Main(string[] args) {
            int szar = 0;
            bool valid = false;
            while(!valid) {
                Console.Clear();
                Console.Write("Generate random files? Y/N : ");
                switch(Console.ReadKey().Key.ToString()) {
                    case "Y":
                        int count = 0;
                        while(!valid) {
                            Console.Clear();
                            Console.Write("Quantity: ");
                            valid = int.TryParse(Console.ReadLine(), out count);
                        }
                        Generate(count);
                        break;
                    case "N": valid = true; break;
                }
            }

            valid = false;
            while (!valid)  {
                Console.Clear();
                string[] files = Directory.GetFiles(Directory.GetCurrentDirectory()).Where(s => !s.EndsWith(".exe") && !s.EndsWith(".dll")).ToArray();
                Console.Write($"Rename {files.Count()} files? Y/N : ");
                switch (Console.ReadKey().Key.ToString()) {
                    case "Y": Rename(files); valid = true; break;
                    case "N": valid = true; break;
                }
            }
        }
        
        static void Generate(int count) {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string[] exts = {"png","jpg","txt","pdf","md","pkt","mp4","mov","zip","torrent","ct","jpeg","rar","img","mp3","mkv","avi","ico","webm"};
            Random r = new Random();
            for (int f=0; f<count; f++) {
                string line = "";
                for (int nameLength=0; nameLength<20; nameLength++) {
                    line += chars[r.Next(0,chars.Length-1)];
                }
                using StreamWriter wr = File.CreateText($@"{line}.{exts[r.Next(0,exts.Length)]}");
                wr.WriteLine(line);
            }
        }
        static void Rename(string[] files) {
            for (int a = 0; a < files.Length; a++) {
                File.Move($@"{files[a]}", $@"{files[a].Split('.').Last()}_{a:D20}.{files[a].Split('.').Last()}");
            }
        }
    }
}
