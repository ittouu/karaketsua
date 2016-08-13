﻿using System.IO;


/// <summary>
/// ディレクトリを操作するクラス
/// NOTE:http://kan-kikuchi.hatenablog.com/entry/DirectoryProcessor
/// </summary>
public static class DirectoryProcessor
{

    /// <summary>
    /// ディレクトリとその中身を上書きコピー
    /// </summary>
    public static void CopyAndReplace(string sourcePath,string copyPath)
    {
        //既にディレクトリがある場合は削除し、新たにディレクトリ作成
        Delete(copyPath);
        Directory.CreateDirectory(copyPath);

        //ファイルをコピー
        foreach(var file in Directory.GetFiles(sourcePath)) {
            File.Copy(file,Path.Combine(copyPath,Path.GetFileName(file)));
        }

        //ディレクトリの中のディレクトリも再帰的にコピー
        foreach(var dir in Directory.GetDirectories(sourcePath)) {
            CopyAndReplace(dir,Path.Combine(copyPath,Path.GetFileName(dir)));
        }
    }

    /// <summary>
    /// 指定したディレクトリとその中身を全て削除する
    /// </summary>
    public static void Delete(string targetDirectoryPath)
    {
        if(!Directory.Exists(targetDirectoryPath)) {
            return;
        }

        //ディレクトリ以外の全ファイルを削除
        string[] filePaths = Directory.GetFiles(targetDirectoryPath);
        foreach(string filePath in filePaths) {
            File.SetAttributes(filePath,FileAttributes.Normal);
            File.Delete(filePath);
        }

        //ディレクトリの中のディレクトリも再帰的に削除
        string[] directoryPaths = Directory.GetDirectories(targetDirectoryPath);
        foreach(string directoryPath in directoryPaths) {
            Delete(directoryPath);
        }

        //中が空になったらディレクトリ自身も削除
        Directory.Delete(targetDirectoryPath,false);
    }

}