using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Surface.Core;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class TagManager
    {
        private static TagManager instance = null;
        private List<long> usedTags;
        private Dictionary<CoordHelper.EProfile, long> nextTag;

        private TagManager()
        {
            usedTags = new List<long>();
            nextTag = new Dictionary<CoordHelper.EProfile, long>() { { CoordHelper.EProfile.ONEPLAYER, -1 }, { CoordHelper.EProfile.TWOPLAYER, -1 } };
        }

        public static TagManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TagManager();
                return instance;
            }
        }

        public void addTag(TouchPoint touchPoint)
        {
            if (SceneManager.Instance.HasScene(SceneManager.EScene.SOLO) || SceneManager.Instance.HasScene(SceneManager.EScene.MULTI))
            {
                if (CoordHelper.Instance.getNextValueBox(CoordHelper.EProfile.ONEPLAYER).Contains(new Point((int)touchPoint.CenterX, (int)touchPoint.CenterY))) //HITTEST WITH P1 NEXT BOX
                    nextTag[CoordHelper.EProfile.ONEPLAYER] = touchPoint.Tag.Value;
                else if (CoordHelper.Instance.getNextValueBox(CoordHelper.EProfile.TWOPLAYER).Contains(new Point((int)touchPoint.CenterX, (int)touchPoint.CenterY))) //HITTEST WITH P1 NEXT BOX
                    nextTag[CoordHelper.EProfile.TWOPLAYER] = touchPoint.Tag.Value;
            }
        }

        public bool NextTagIsPlace(CoordHelper.EProfile p)
        {
            if (!usedTags.Contains(nextTag[p]) && nextTag[p] != -1)
                return true;
            return false;
        }

        public long getNextTag(CoordHelper.EProfile p)
        {
            long ret = nextTag[p];

            usedTags.Add(nextTag[p]);
            return ret;
        }
    }
}
