using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    public class MenuManager
    {
        private static MenuManager instance = null;
        private Stack<AScene> scenes = null;

        private MenuManager()
        {
            scenes = new Stack<AScene>();
        }

        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MenuManager();
                return instance;
            }
        }
    }
}
