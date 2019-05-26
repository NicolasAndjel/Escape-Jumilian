using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : PlayerElement
{
    bool didJumpAgain;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        didJumpAgain = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        DoubleJump();        
    }

    void DoubleJump()
    {
        if (!ph.pm.grounded)
        {          
            if (Input.GetKeyDown(ph.pm.jumButton))
            {
                ph.pm.DoubleJump(true);
                dm.health -= 20;
            }
        }
    }
    
}

