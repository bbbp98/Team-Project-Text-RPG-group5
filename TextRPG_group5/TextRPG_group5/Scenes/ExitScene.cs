using System;
using System.Threading.Tasks;
using TextRPG_group5.Scenes;


namespace TextRPG_group5.Scenes
{
    internal class ExitScene : Exit

    {
        private static ExitScene scene;
        private static GameProgress gameProgress;

        public bool ShouldExitGame { get; private set; } = false;
        public static ExitScene GetInstance(GameProgress gp)
        {
            if (scene == null)
            {
                scene = new ExitScene();
            }
            gameProgress = gp;
            return scene;
        }
    }
}
