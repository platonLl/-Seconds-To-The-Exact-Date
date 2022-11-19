using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace STTED
{
    public enum ComponentType
    {
      Year,
      Day,
      Hour,
      Minute,
      Second
    }
    public class Program
    {
        public static string formatDuration(int seconds)
        {
            if (seconds == 0) return "now";

            var components = new List<string>();
            foreach (ComponentType type in Enum.GetValues(typeof(ComponentType)))
            {
                var component = GenerateComponent(seconds, type);

                if(component != null)
                {
                    components.Add(component);
                }
            }
            var index = 0;
            var result = string.Empty;
            foreach (string component in components)
            {
                index++;
                var isLastComponent = components.Count == index;
                var isBeforeLast = components.Count - 1 == index;
                if (isLastComponent)
                {
                    result += $"{component}";
                }
                else if(isBeforeLast)
                {
                    result += $"{component} and ";
                }
                else
                {
                    result += $"{component}, ";
                }
            }
            return result;
        }

        public static string GenerateComponent(int seconds, ComponentType type)
        {
            var minuteSeconds = 60;
            var hourSeconds = minuteSeconds * 60;
            var daySeconds = hourSeconds * 24;
            var yearSeconds = daySeconds * 365;
            var itemCount = 0;

            switch (type)
            {
                case ComponentType.Second:
                    {
                        itemCount = seconds % 60;
                        break;
                    }
                case ComponentType.Minute:
                    {
                        itemCount = (seconds % hourSeconds) / minuteSeconds;
                        break;
                    }
                case ComponentType.Hour:
                    {
                        itemCount = (seconds % daySeconds) / hourSeconds;
                        break;
                    }
                case ComponentType.Day:
                    {
                        itemCount = (seconds % yearSeconds) / daySeconds;
                        break;
                    }

                case ComponentType.Year:
                    {
                        itemCount = (seconds / yearSeconds);
                        break;
                    }
            }
            if (itemCount == 0)
                return null;

            return $"{itemCount} {type.ToString().ToLower()}{(itemCount == 1 ? "" : "s")}";

        }
    }
}