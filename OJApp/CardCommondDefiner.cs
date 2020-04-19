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
        public static void IncreaseHealth(GameContext context, int value)
        {
            context.hostCard.health += value;
        }
        public static void Log(GameContext context, object message)
        {
            Console.WriteLine(message.ToString());
        }
        public static void LogInt(GameContext context, int message)
        {
            Console.WriteLine(message);
        }

        public static int Health(GameContext context)
        {
            return context.hostCard.health;
        }

        public static void If(GameContext context, IOJMethod condition, IOJMethod action)
        {
            if ((bool)condition.Invoke(context))
            {
                action.Invoke(context);
            }
        }

        public static void Ifelse(GameContext context, IOJMethod condition, IOJMethod action, IOJMethod other)
        {
            if ((bool)condition.Invoke(context))
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
            return new List<Card>() { new Card() { health = 1 }, new Card() { health = 1 } };
        }


        public static bool LargeThen(GameContext context, int value, int then)
        {
            return value > then;

        }

        public static void SetHealth(GameContext context, Card card, int value)
        {
            card.health = value;
        }
        public static int GetHealth(GameContext context, Card card)
        {
            return card.health;
        }
    }
}
