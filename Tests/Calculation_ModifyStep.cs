using Calculator.Models;

namespace Tests
{
	public class Calculation_ModifyStep
	{
        [Fact]
        public void ModifyStepShould_5_plus_3_nvm_4_eq_9()
        {
            Step firstStep = new(5m, Calculator.Operation.ADDITION);
            Calculation calculation = new(firstStep);
            var three = new Step(3m);
            calculation.AddStep(three);
            calculation.ModifyStep(three, new Step(4m));
            var solution = calculation.ComputeSolution();
            Assert.Equal(9m, solution);
        }
    }
}

