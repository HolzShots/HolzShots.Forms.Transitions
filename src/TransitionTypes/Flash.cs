namespace HolzShots.Forms.Transitions.TransitionTypes;

/// <summary>
/// This transition type 'flashes' the properties a specified number of times, ending
/// up by reverting them to their initial values. You specify the number of bounces and
/// the length of each bounce.
/// </summary>
public class Flash : UserDefined
{
    #region Public methods

    /// <summary>
    /// You specify the number of bounces and the time taken for each bounce.
    /// </summary>
    public Flash(int iNumberOfFlashes, int iFlashTime)
    {
        // This class is derived from the user-defined transition type.
        // Here we set up a custom "user-defined" transition for the
        // number of flashes passed in...
        var dFlashInterval = 100.0f / iNumberOfFlashes;

        // We set up the elements of the user-defined transition...
        var elements = new List<TransitionElement>();
        for (int i = 0; i < iNumberOfFlashes; ++i)
        {
            // Each flash consists of two elements: one going to the destination value,
            // and another going back again...
            var dFlashStartTime = i * dFlashInterval;
            var flashEndTime = dFlashStartTime + dFlashInterval;
            var flashMidPoint = (dFlashStartTime + flashEndTime) / 2.0f;
            elements.Add(new(flashMidPoint, 100, InterpolationMethod.EaseInEaseOut));
            elements.Add(new(flashEndTime, 0, InterpolationMethod.EaseInEaseOut));
        }

        Setup(elements, iFlashTime * iNumberOfFlashes);
    }

    #endregion

}
