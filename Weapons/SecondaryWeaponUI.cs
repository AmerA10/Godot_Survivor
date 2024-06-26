using Godot;
using System;

public class SecondaryWeaponUI : TextureProgress
{

    private Texture overTexture;

    public override void _Ready()
    {
        this.Value = 0;
        this.TintOver = new Color(this.TintOver.a, this.TintOver.g, this.TintOver.b, 0.0f);
        this.Step= 1.0f;   
    }

    public void SetUpProgress(int maxAmount)
    {
        this.MaxValue= maxAmount;
    }

    public void ResetProgress() 
    {
        Value = 0;
    }

    public void UpdateAmount(int amount)
    {
        this.Value = amount;
        if(Value == this.MaxValue)
        {
            this.TintOver = new Color(this.TintOver.a, this.TintOver.g, this.TintOver.b, 0.8f);
        }
        else 
        {
            this.TintOver = new Color(this.TintOver.a, this.TintOver.g, this.TintOver.b, 0.0f);
        }
    }
}
