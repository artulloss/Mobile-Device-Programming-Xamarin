using Calculator.Models;

namespace Tests;

public class Calculation_ComputeSolution
{
    [Fact]
    public void ComputeSolutionShould_5_plus_5_eq_10()
    {
        Step firstStep = new(5m, Calculator.Operation.ADDITION);
        Calculation calculation = new(firstStep);
        calculation.AddStep(new Step(5m));
        var solution = calculation.ComputeSolution();
        Assert.Equal(10m, solution);
    }

    [Fact]
    public void ComputeSolutionShould_10_div_5_plus_4_eq_6_plus_4_eq_10()
    {
        Step firstStep = new(10m, Calculator.Operation.DIVISION);
        Calculation calculation = new(firstStep);
        var solution = calculation
            .AddStep(new Step(5m, Calculator.Operation.ADDITION))
            .AddStep(new Step(4m, Calculator.Operation.ADDITION))
            .ComputeSolution();

        Assert.Equal(6m, solution);

        solution = calculation.AddStep(new Step(4m)).ComputeSolution();
        Assert.Equal(10m, solution);
    }
}
