using System;
using NetEXT.Input;
using NetEXT.TimeFunctions;
using SFML.Graphics;
using SFML.Window;
using testlol.World;
using Action = NetEXT.Input.Action;

namespace testlol.States
{
    class MenuState : GameState
    {
         


        Text t = new Text("Menu State", Game.Font);

        private TileMap testMap;

        public MenuState(StateMachine machine, RenderWindow window, bool replace = true) : base(machine, window, replace)
        {
            Map t = Map.LoadMap("test.map");
            testMap = new TileMap(new Texture("tileset.png"),new Vector2u(32,32) , t );
            Console.WriteLine("menu created");
            testMap.Scale = new Vector2f(2,2);

            TestMap[Actions.Enter] = new Action(Keyboard.Key.Return, ActionType.PressOnce);
            TestMap[Actions.Quit] = new Action(Keyboard.Key.Escape, ActionType.ReleaseOnce) | new Action(EventType.Closed);
            
            EventSystem.Connect(Actions.Enter, (c) => Next = StateMachine.BuildState<PlayState>(Machine, Window, true));
            EventSystem.Connect(Actions.Quit, (c) => Machine.Running = false);

        }

        public override void Pause()
        {
            
        }

        public override void Resume()
        {
            
        }

        public override void Update(Time dt)
        {

        }

        public override void ProcessEvents()
        {
            base.ProcessEvents();
        }

        public override void Draw()
        {
            Window.Clear();
            Window.Draw(testMap);
            Window.Draw(t);
            Window.Display();
        }

    }
}
