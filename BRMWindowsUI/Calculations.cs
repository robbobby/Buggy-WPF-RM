using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace BRMWindowsUI {
    public class Calculations : ICalculations {
        public List<string> Register { get; set; } = new List<string>();

        public double Add(double x, double y) {
            return x + y;
        }
    }
}