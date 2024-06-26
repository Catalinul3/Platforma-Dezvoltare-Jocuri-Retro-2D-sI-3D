using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworkFor3D.helpers
{
    public static class FileHelpers
    {
        public static string LoadObjectDialog(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "3d files (*.obj)| *obj"
            };

            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;

            return fileDialog.FileName;
        }
        public static string LoadMaterialDialog(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "material (*.mtl)| *mtl"
            };

            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;

            return fileDialog.FileName;
        }
        public static string LoadImageDialog(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "material (*.jpg,*png,*jpeg)| *jpg;*png;*jpeg"
            };

            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;

            return fileDialog.FileName;
        }
        public static string[] LoadSoundDialog(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "Sounds Files ( *wav)| ; *wav"
                ,
                Multiselect = true
            };
            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;
            return fileDialog.FileNames;
        }
        public static string[] LoadMusicDialog(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "Music Files (*wav, *mp3, *mp4)| *wav;*mp3;*mp4",
                Multiselect = true
            };
            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;
            return fileDialog.FileNames;
        }
        public static string loadFBXDialog(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "FBX files (*.fbx)| *fbx"

            };
            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;
            return fileDialog.FileName;
        }
    }
}
