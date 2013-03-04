using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TetriClimber
{
    class ScoreBoard : DrawableGameComponent
    {
        private String filename;
        List<Score> scores;

        public ScoreBoard() :
            base(App.Game)
        {
            filename = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TetriClimber/scoreboard.sav";
            scores = new List<Score>();
            loadScoreBoard();
        }

        public void addScore(Score s)
        {
            scores.Add(s);
            saveScoreBoard();
        }

        public void Dump()
        {
            foreach(Score s in scores)
                Console.WriteLine(""+s.pseudo+" "+s.TotalScore);
        }

        private void saveScoreBoard()
        {
            try
            {
                FileStream stream = File.Open(filename, FileMode.Create, FileAccess.Write);
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, scores);
                stream.Close();
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/TetriClimber");
                saveScoreBoard();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Impossible de sauvegarder le score board.");
                Console.Error.WriteLine(e.Message);
            }
        }

        private void loadScoreBoard()
        {
            FileStream stream = null;
            try
            {
                stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                BinaryFormatter bin = new BinaryFormatter();
                scores = (List<Score>)bin.Deserialize(stream);
                stream.Close();
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Impossible de charger le score board.");
                if (stream != null)
                    stream.Close();
            }
        }
    }
}
