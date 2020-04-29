using OrderedJson.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OJApp
{
    public static partial class CardCommondDefiner
    {
        public static void StartTurn(GameContext context, IOJMethod method)
        {
            context.hostCard.TurnStart = method;
        }
        public static void IncreaseHealth(GameContext context, int value = 5)
        {
            context.hostCard.health += value;
        }
        public static void Log(GameContext context, object message, object message2 = null)
        {
            string output = message.ToString();
            if (message2 != null)
            {
                output += message2.ToString();
            }
            Console.WriteLine(output);
        }
        public static void LogInt(GameContext context, int message)
        {
            Console.WriteLine(message);
        }

        public static int Health(GameContext context)
        {
            return context.hostCard.health;
        }

        public static void If(GameContext context, bool condition, IOJMethod action)
        {
            if (condition)
            {
                action.Invoke(context);
            }
        }
        public static bool True(GameContext context)
        {
            return true;
        }
        public static bool False(GameContext context)
        {
            return false;
        }

        public static void Ifelse(GameContext context, bool condition, IOJMethod action, IOJMethod other)
        {
            if (condition)
            {
                action.Invoke(context);
            }
            else
            {
                other.Invoke(context);
            }
        }
        public static void Repeat(GameContext context, int times, IOJMethod method)
        {
            for (int i = 0; i < times; i++)
            {
                method.Invoke(context);
            }
        }

        public static void ForCards(GameContext context, List<Card> list, IOJMethod method)
        {
            foreach (var card in list)
            {
                context.cursor = card;
                method.Invoke(context);
            }
        }
        public static Card Cur(GameContext context)
        {
            return context.cursor;
        }

        public static List<Card> AllenemyCards(GameContext context)
        {
            return context.enemies;
        }


        public static bool LargeThen(GameContext context, int value, int then)
        {
            return value > then;

        }

        public static void Do(GameContext gameContext, IOJMethod oJMethod1, IOJMethod oJMethod2)
        {
            // 改装成可变参数
        }

        public static void SetHealth(GameContext context, Card card, int value)
        {
            if (card != null)
            {
                card.health = value;
            }
        }

        public static Card GetNullCard(GameContext context)
        {
            return null;
        }

        public static bool Equals(GameContext context, object obj1, object obj2)
        {
            if (obj1 is IOJMethod method1)
            {
                obj1 = method1.Invoke(context);
            }
            if (obj2 is IOJMethod method2)
            {
                obj2 = method2.Invoke(context);
            }
            if (obj1 == obj2) return true;
            if (obj1.Equals(obj2)) return true;
            return false;
        }

        public static int GetHealth(GameContext context, Card card)
        {
            return card.health;
        }
    }
}
