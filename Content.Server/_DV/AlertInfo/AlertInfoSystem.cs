using Content.Server.Station.Systems;

namespace Content.Server._DV.AlertInfo;

/// <summary>
/// This handles adding the component to new stations, provides methods to read / write it + an event
/// </summary>
public sealed class AlertInfoSystem : EntitySystem
{
    [Dependency] private readonly StationSystem _stationSystem = default!;

    /// <inheritdoc/>
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<StationInitializedEvent>(OnStationInitialized);
    }

    private void OnStationInitialized(StationInitializedEvent args)
    {
        if (!TryComp<AlertInfoComponent>(args.Station, out var alertInfoComponent))
            return;

        // TODO make this a locale string or hide the field completely if no information is present -
        // TODO but do not hardcode this
        alertInfoComponent.Text = "No information has been shared yet";
    }

    /// <summary>
    /// Sets the alert info that is displayed on all PDAs for a station
    /// </summary>
    public void SetAlertInfo(EntityUid station, string text, AlertInfoComponent? component = null)
    {
        if (!Resolve(station, ref component))
            return;

        if (component.Text == text)
            return;

        RaiseLocalEvent(new StationAlertInfoChangedEvent(station, text));
    }

    /// <summary>
    /// Gets the alert info for a given station
    /// </summary>
    public string GetAlertInfo(EntityUid station, AlertInfoComponent? component = null)
    {
        return !Resolve(station, ref component) ? string.Empty : component.Text;
    }
}

/// <summary>
/// Event raised when the alert info changes
/// </summary>
public sealed class StationAlertInfoChangedEvent : EntityEventArgs
{
    public EntityUid Station { get; }
    public string AlertInfo { get; }

    public StationAlertInfoChangedEvent(EntityUid station, string alertInfo)
    {
        Station = station;
        AlertInfo = alertInfo;
    }
}
