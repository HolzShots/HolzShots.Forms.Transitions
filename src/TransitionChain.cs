namespace HolzShots.Forms.Transitions;

internal class TransitionChain
{
    private readonly LinkedList<Transition> _listTransitions = new();

    public TransitionChain(params Transition[] transitions)
    {
        foreach (var transition in transitions)
            _listTransitions.AddLast(transition);
        RunNextTransition();
    }

    private void RunNextTransition()
    {
        if (_listTransitions.Count == 0)
            return;

        // We find the next transition and run it. We also register
        // for its completed event, so that we can start the next transition
        // when this one completes...
        Transition nextTransition = _listTransitions.First.Value;
        nextTransition.TransitionCompletedEvent += OnTransitionCompleted;
        nextTransition.Run();
    }

    private void OnTransitionCompleted(object sender, Transition.Args e)
    {
        // We unregister from the completed event...
        var transition = (Transition)sender;
        transition.TransitionCompletedEvent -= OnTransitionCompleted;

        // We remove the completed transition from our collection, and
        // run the next one...
        _listTransitions.RemoveFirst();
        RunNextTransition();
    }
}
