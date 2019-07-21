using System;

namespace PokerTest
{
    public static class CardTypes
    {
        //I am assuming that ace is higher than king, king is higher than queen, queen is higher than jack and jack is higher than 10.
        //  I am also assuming that I don't need to keep the original input value
        public enum Ranks { two = 2, three = 3, four = 4, five = 5, six = 6, seven = 7, eight = 8, nine = 9, ten = 10, jack = 11, queen = 12, king = 13, ace = 14 }
        public enum Suits { hearts = 'h', spades = 's', clubs = 'c', diamonds = 'd' }

        public static Ranks GetFaceCard(string value)
        {
            Ranks rank;

            switch (value.ToLower())
            {
                case "j":
                    rank = Ranks.jack;
                    break;
                case "q":
                    rank = Ranks.queen;
                    break;
                case "k":
                    rank = Ranks.king;
                    break;
                case "a":
                    rank = Ranks.ace;
                    break;
                default:
                    throw new FormatException("Invalid Face Card");
            }

            return rank;
        }
    }
}
