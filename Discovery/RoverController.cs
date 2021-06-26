using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery
{
    public class RoverController
    {
        public string MoveRovers(string input)
        {
            PlanetDimention dimentions;
            List<Rover> rovers = new List<Rover>();

            try
            {
                string[] inputList = input.Split("\n");

                if (inputList == null || inputList.Length < 3)
                {
                    return "Huston! Something wwent wrong!";
                }

                dimentions = readPLanetDimentions(inputList[0]);

                if (dimentions == null || dimentions.ReadSuccessCode != 0)
                {
                    return dimentions != null ? dimentions.ReadSuccessDesc : "Huston! Something wwent wrong!";
                }

                for (int i = 1; i <= (inputList.Length - 1) / 2; i++)
                {
                    Rover rover = new Rover();
                    rover.RoverCoordinates = getRoverCoordinates(inputList[(i * 2) - 1]);
                    rover.RoverOrders = inputList[i * 2].Trim();

                    rovers.Add(rover);
                }

                List<Rover> roverCoordinates = discoverPlanet(rovers, dimentions);

                StringBuilder output = new StringBuilder();

                foreach (Rover item in roverCoordinates)
                {
                    if (string.IsNullOrEmpty(item.RoverMessage))
                    {
                        output.Append(item.RoverCoordinates.X.ToString() + " " +
                            item.RoverCoordinates.Y.ToString() + " " +
                            item.RoverCoordinates.Direction + Environment.NewLine);
                    }
                    else
                    {
                        output.Append(item.RoverMessage);
                    }
                }

                return output.ToString();

            }
            catch
            {
                return "Huston! Something wwent wrong!";
            }
        }

        private List<Rover> discoverPlanet(List<Rover> rovers, PlanetDimention dimentions)
        {
            foreach (Rover rover in rovers)
            {
                foreach (char roverOrder in rover.RoverOrders)
                {
                    if (roverOrder == 'L' || roverOrder == 'R')
                    {
                        rover.RoverCoordinates.Direction = changeRroverDirection(rover.RoverCoordinates.Direction, roverOrder.ToString());
                    }

                    if (roverOrder == 'M')
                    {
                        rover.RoverCoordinates = moveRover(rover.RoverCoordinates);

                        if (isRoverMovedOutOfPlanet(rover.RoverCoordinates, dimentions))
                        {
                            rover.RoverMessage = "Huston! Im out! See u!";
                        }
                    }
                }

            }

            return rovers;
        }

        private RoverCoordnates moveRover(RoverCoordnates roverCoord)
        {
            switch (roverCoord.Direction)
            {
                case "E":
                    roverCoord.X = roverCoord.X + 1;
                    return roverCoord;
                case "W":
                    roverCoord.X = roverCoord.X - 1;
                    return roverCoord;
                case "N":
                    roverCoord.Y = roverCoord.Y + 1;
                    return roverCoord;
                case "S":
                    roverCoord.Y = roverCoord.Y - 1;
                    return roverCoord;
                default:
                    break;
            }

            return roverCoord;
        }

        private string changeRroverDirection(string roverDirection, string turningDirection)
        {
            if (turningDirection == "L")
            {
                switch (roverDirection)
                {
                    case "E":
                        return "N";
                    case "W":
                        return "S";
                    case "N":
                        return "W";
                    case "S":
                        return "E";
                    default:
                        break;
                }
            }

            if (turningDirection == "R")
            {
                switch (roverDirection)
                {
                    case "E":
                        return "S";
                    case "W":
                        return "N";
                    case "N":
                        return "E";
                    case "S":
                        return "W";
                    default:
                        break;
                }
            }

            return string.Empty;
        }

        private bool isRoverMovedOutOfPlanet(RoverCoordnates coords, PlanetDimention dimentions)
        {
            if (coords.X > dimentions.Width)
            {
                return true;
            }

            if (coords.Y > dimentions.Height)
            {
                return true;
            }

            return false;
        }

        private RoverCoordnates getRoverCoordinates(string input)
        {
            RoverCoordnates coord = new RoverCoordnates();

            try
            {
                string[] coordinates = input.Split(" ");

                if (coordinates == null || coordinates.Length < 3)
                {
                    coord.CoordinateReadSucces = 1;
                }

                coord.X = Convert.ToInt32(coordinates[0].Trim());
                coord.Y = Convert.ToInt32(coordinates[1].Trim());
                coord.Direction = coordinates[2].Trim();
                coord.CoordinateReadSucces = 0;
            }
            catch
            {
                coord.CoordinateReadSucces = 9;
            }

            return coord;
        }

        private PlanetDimention readPLanetDimentions(string input)
        {
            try
            {
                string[] inputList = input.Split(" ");

                if (inputList != null && inputList.Length > 1)
                {
                    PlanetDimention dimentions = new PlanetDimention();
                    dimentions.Width = Convert.ToInt32(inputList[0].Trim());
                    dimentions.Height = Convert.ToInt32(inputList[1].Trim());
                    dimentions.ReadSuccessCode = 0;
                    dimentions.ReadSuccessDesc = "OK";

                    return dimentions;
                }
                else
                {
                    return new PlanetDimention() { ReadSuccessCode = 1, ReadSuccessDesc = "Huston! Something wwent wrong!" };
                }
            }
            catch
            {
                return new PlanetDimention() { ReadSuccessCode = 2, ReadSuccessDesc = "Huston! Something wwent wrong!" };
            }

        }

        private class PlanetDimention
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public int ReadSuccessCode { get; set; }
            public string ReadSuccessDesc { get; set; }

        }

        private class RoverCoordnates
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string Direction { get; set; }
            public int CoordinateReadSucces { get; set; }
        }

        private class Rover
        {
            public RoverCoordnates RoverCoordinates { get; set; }
            public string RoverOrders { get; set; }
            public string RoverMessage { get; set; }

        }
    }
}
