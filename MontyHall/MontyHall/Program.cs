using System;

namespace MontyHall
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var noAttempts = 10000;
            var noCarsSwitch = 0;
            var noCarsStay = 0;

            var game = new Game(new Random());
            for (var i = 0; i < noAttempts; i++)
            {
                var successOnSwitch = game.Play();

                if (successOnSwitch)
                {
                    noCarsSwitch++;
                }
                else
                {
                    noCarsStay++;
                }

                OutputRunningStats(noAttempts, noCarsStay, noCarsSwitch);
            }

            Console.Read();
        }

        private static void OutputRunningStats(int noAttempts, int noCarsStay, int noCarsSwitch)
        {
            var winner = (noCarsStay > noCarsSwitch) ? "STAY" : "SWITCH";
            var output = string.Format("{0} attempts: {1} stayed, {2} switched. WINNING : {3}", noAttempts, noCarsStay, noCarsSwitch, winner);
            Console.Clear();
            Console.Write(output);
        }
    }

    internal class Game
    {
        public char DoorWithCar { get; set; }

        public char FirstSelectedDoor { get; set; }

        public char GoatyDoor { get; set; }

        public char SwitchedDoor { get; set; }

        private Random randObj;

        public Game(Random randObj)
        {
            this.randObj = randObj;
        }

        public bool Play()
        {
            // put car behind a door
            SetupGame();

            // randomly pick a door
            PickFirstDoor();

            // open other door which has goat
            OpenGoatyDoor();

            // switch to other door.
            SwitchDoor();
            // return outcome of Switch
            return ReturnSwitchOutcome();
        }

        public void SetupGame()
        {
            DoorWithCar = GetRandomDoor();
        }

        public void PickFirstDoor()
        {
            FirstSelectedDoor = GetRandomDoor();
        }

        public char GetRandomDoor()
        {
            var randomDoor = '\0';
            switch (randObj.Next(1, 4))
            {
                case 1:
                    randomDoor = 'A';
                    break;

                case 2:
                    randomDoor = 'B';
                    break;

                case 3:
                    randomDoor = 'C';
                    break;
            }
            return randomDoor;
        }

        public void OpenGoatyDoor()
        {
            var randomDoor = GetRandomDoor();
            while (randomDoor == FirstSelectedDoor || randomDoor == DoorWithCar)
            {
                randomDoor = GetRandomDoor();
            }

            GoatyDoor = randomDoor;
        }

        public void SwitchDoor()
        {
            if ('A' != FirstSelectedDoor && 'A' != GoatyDoor) SwitchedDoor = 'A';
            if ('B' != FirstSelectedDoor && 'B' != GoatyDoor) SwitchedDoor = 'B';
            if ('C' != FirstSelectedDoor && 'C' != GoatyDoor) SwitchedDoor = 'C';
        }

        public bool ReturnSwitchOutcome()
        {
            return (SwitchedDoor == DoorWithCar);
        }
    }
}