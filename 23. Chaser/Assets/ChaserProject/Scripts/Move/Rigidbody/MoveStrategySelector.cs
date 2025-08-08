using System.Collections.Generic;

public class MoveStrategySelector
{
    private GroundMoveSensor _moveSensor;
    private List<StrategyBase> _strategies;
    
    public MoveStrategySelector(GroundMoveSensor sensor, List<StrategyBase> strategies)
    {
        _strategies = strategies;
        _moveSensor = sensor;
    }
    
    public bool TrySelect(out StrategyBase selectedStrategy)
    {
        _moveSensor.Probe();
        return TryChooseStrategy(out selectedStrategy);
    }
    
    private bool TryChooseStrategy(out StrategyBase selectedStrategy)
    {
        selectedStrategy = null;
        
        foreach (StrategyBase strategy in _strategies)
        {
            if (strategy.Evaluate(_moveSensor) == false)
                continue;
            
            selectedStrategy = strategy;
            return true;
        }
        
        return false;
    }
}
