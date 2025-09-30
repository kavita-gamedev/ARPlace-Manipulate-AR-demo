using UnityEngine;
using System;

public interface IPopup
{
    /// <summary>
    /// Enables the popup.
    /// </summary>
    void EnablePopup();

    /// <summary>
    /// Disables the popup.
    /// </summary>
    void DisablePopup();

    /// <summary>
    /// Sets the popup with a mesage.
    /// </summary>
    /// <param name="message">Message.</param>
    void SetPopup(string message);

    /// <summary>
    /// Sets the popup with custom message.
    /// </summary>
    /// <param name="message">Message.</param>
    /// <param name="disableButton">If set to <c>true</c> disable button.</param>
    void SetPopup(string message, bool disableButton);

    /// <summary>
    /// Sets the popup.
    /// </summary>
    /// <param name="displayTex">Display tex.</param>
    /// <param name="message">Message.</param>
    /// <param name="disableButton">If set to <c>true</c> disable button.</param>
	void SetPopup(Material material, Sprite displayTex, string btnText, string message, bool disableBtn, Action mAction, bool disableCloseButton,bool isRewardGold);

    /// <summary>
    /// Raises the back button pressed event.
    /// </summary>
    void OnBackButtonPressed();

    /// <summary>
    /// Gets a value indicating whether this instance is visible.
    /// </summary>
    /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
    bool IsVisible { get; }

}
