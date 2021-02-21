using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ExecFfmpegBat
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                // ★ストリーミングファイルを動画ファイルに戻す
                // FFMPEGを実行するbatファイルの名前を取得する
                var fileNames = Directory.EnumerateFiles(Properties.Resources.DirectoryBatfiles, "*.bat");
                var count = 0;
                var listCount = fileNames.ToList().Count;
                foreach (var fileName in fileNames)
                {
                    // 動画を50ファイル出力したら終了する
                    // ストレージを圧迫するため。
                    if(count > 300)
                    {
                        break;
                    }

                    Console.WriteLine(String.Format("{0}/{1}:{2}", ++count, listCount, fileName));

                    // FFMPEGを起動するためのコマンドを設定
                    var processInfo = new ProcessStartInfo();
                    processInfo.WorkingDirectory = Properties.Resources.DirectoryBatfiles;
                    processInfo.FileName = fileName;

                    // FFMPEGを起動して動画ファイルを作成
                    // FFMPEGのプロセスが完了するまでwaitする
                    var process = Process.Start(processInfo);
                    process.WaitForExit();

                    // 使用したbatファイルをtrushディレクトリに移動
                    File.Move(fileName, Properties.Resources.DirectoryTrush + Path.GetFileName(fileName));

                }
                /*
                // ★動画ファイルを動画置き場に移動する
                // FFMPEGを実行するbatファイルの名前を取得する
                fileNames = Directory.EnumerateFiles(Properties.Resources.DirectoryOutput, "*");
                count = 0;
                listCount = fileNames.ToList().Count;
                foreach (var fileName in fileNames)
                {
                    Console.WriteLine(String.Format("{0}/{1}:{2}", ++count, listCount, fileName));
                    File.Move(
                        Properties.Resources.DirectoryOutput + Path.GetFileName(fileName),
                        Properties.Resources.DirectoryMoveto + Path.GetFileName(fileName)
                    );
                }
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}