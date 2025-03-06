using Robust.Shared.GameStates;

namespace Content.Server._DV.AlertInfo;

/// <summary>
/// This component is attached to each station storing an arbitrary text that is displayed on the PDAs
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class AlertInfoComponent : Component
{
    /// <summary>
    ///  The text that is displayed on the PDA-s
    /// </summary>
    [ViewVariables(VVAccess.ReadOnly)]
    [AutoNetworkedField]
    public string Text = string.Empty;
}
