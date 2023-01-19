using System;

namespace Nickmiste.UltimateMathLibrary {

    /// <summary> Represents a set of dice that can be rolled. Supports dice notation (e.g., 2d6+11). </summary>
    [Serializable]
    public class DiceRoll {

        private int _numDice;
        /// <summary> The number of dice to roll. </summary>
        public int numDice {
            get => _numDice;
            set {
                if (value < 1) throw new ArgumentException("numDice must be at least 1.", nameof(value));
                _numDice = value;
            }
        }

        private int _numFaces;
        /// <summary> The number of faces on each die. </summary>
        public int numFaces {
            get => _numFaces;
            set {
                if (value < 2) throw new ArgumentException("numFaces must be at least 2.", nameof(value));
                _numFaces = value;
            }
        }

        /// <summary> This value is added the the final dice roll. </summary>
        public int additiveModifier { get; set; }

        /// <summary> Create a new dice roll. </summary>
        /// <param name="numDice"> <inheritdoc cref="numDice" path="/summary"/> </param>
        /// <param name="numFaces"> <inheritdoc cref="numFaces" path="/summary"/> </param>
        /// <param name="additiveModifier"> <inheritdoc cref="additiveModifier" path="/summary"/> </param>
        public DiceRoll(int numDice = 1, int numFaces = 6, int additiveModifier = 0) {
            this.numDice = numDice;
            this.numFaces = numFaces;
            this.additiveModifier = additiveModifier;
        }

        /// <summary> Create a new dice roll using dice notation. </summary>
        /// <param name="notation"> The dice notation of the roll (e.g., 2d6+11). </param>
        /// <exception cref="ArgumentException"> Thrown when the dice notation is invalid. </exception>
        public DiceRoll(string notation) {
            notation = System.Text.RegularExpressions.Regex.Replace(notation, @"\s+", "");

            //Parse numDice
            int d = notation.IndexOfAny(new[] { 'd', 'D' });
            if (d == -1) throw InvalidNotation();
            string numDiceStr = notation.Substring(0, d);
            if (numDiceStr.Equals(""))
                numDice = 1;
            else if (int.TryParse(numDiceStr, out int numDice))
                this.numDice = numDice;
            else
                throw InvalidNotation();

            //Parse additiveModifier
            int op = notation.IndexOfAny(new[] { '+', '-' });
            if (op == -1)
                additiveModifier = 0;
            else if (int.TryParse(notation.Substring(op + 1), out int additiveModifier))
                this.additiveModifier = additiveModifier * (notation[op] == '+' ? 1 : -1);
            else
                throw InvalidNotation();

            //Parse numFaces
            string numFacesStr = op == -1 ?
                notation.Substring(d + 1) :
                notation.Substring(d + 1, op - d - 1);
            if (int.TryParse(numFacesStr, out int numFaces))
                this.numFaces = numFaces;
            else
                throw InvalidNotation();

            ArgumentException InvalidNotation() =>
                new ArgumentException($"Invalid dice notation: {notation}", nameof(notation));
        }

        /// <summary> <inheritdoc cref="Roll(out int[])" path="/summary"/> </summary>
        /// <returns> <inheritdoc cref="Roll(out int[])" path="/returns"/> </returns>
        public int Roll() => Roll(out int[] _);

        /// <summary> Roll the dice. </summary>
        /// <param name="results"> An array containing each individual dice roll. </param>
        /// <returns> The sum of each of the dice rolled, plus the additive modifier (if any). </returns>
        public int Roll(out int[] results) {
            results = new int[numFaces];
            int total = additiveModifier;
            for (int i = 0; i < numDice; i++)
                total += results[i] = UnityEngine.Random.Range(1, numFaces + 1);
            return total;
        }

        public override string ToString() {
            var stringBuilder = new System.Text.StringBuilder();
            if (numDice > 1)
                stringBuilder.Append(numDice);
            stringBuilder.Append('d').Append(numFaces);
            if (additiveModifier != 0)
                stringBuilder.Append(additiveModifier > 0 ? "+" : "").Append(additiveModifier);
            return stringBuilder.ToString();
        }

        public override bool Equals(object obj) {
            return obj is DiceRoll roll &&
                   numDice == roll.numDice &&
                   numFaces == roll.numFaces &&
                   additiveModifier == roll.additiveModifier;
        }

        public override int GetHashCode() {
            int hashCode = -356105133;
            hashCode = hashCode * -1521134295 + numDice.GetHashCode();
            hashCode = hashCode * -1521134295 + numFaces.GetHashCode();
            hashCode = hashCode * -1521134295 + additiveModifier.GetHashCode();
            return hashCode;
        }

    }
}