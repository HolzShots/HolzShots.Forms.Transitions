﻿using System.Collections.Generic;

namespace HolzShots.Forms.Transitions;

internal class TransitionChain
{
    #region Public methods

    public TransitionChain(params Transition[] transitions)
    {
        // We store the list of transitions...
        foreach (var transition in transitions)
            _listTransitions.AddLast(transition);

        // We start running them...
        RunNextTransition();
    }

    #endregion

    #region Private functions

    /// <summary>
    /// Runs the next transition in the list.
    /// </summary>
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

    /// <summary>
    /// Called when the transition we have just run has completed.
    /// </summary>
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

    #endregion

    #region Private data

    // The list of transitions in the chain...
    private readonly LinkedList<Transition> _listTransitions = new LinkedList<Transition>();

    #endregion
}
