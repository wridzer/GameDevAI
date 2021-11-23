using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTExample
{
    public enum BTNodeStatus { Success, Running, Failed }

    public class BTSequence : BTBaseNode
    {
        private int currentIndex = 0;
        private BTBaseNode[] nodes;
        public BTSequence(params BTBaseNode[] inputNodes)
        {
            nodes = inputNodes;
        }

        public override BTNodeStatus Run()
        {
            for(; currentIndex < nodes.Length ; currentIndex++){
            {
                BTNodeStatus result = nodes[currentIndex].OnUpdate();
                switch (result)
                {
                    case BTNodeStatus.Failed: currentIndex = 0; return BTNodeStatus.Failed;
                    case BTNodeStatus.Success: break;
                    case BTNodeStatus.Running: return BTNodeStatus.Running;
                }
            }
            currentIndex = 0;
            return BTNodeStatus.Success;
        }
    }

    public class BTMove : BTBaseNode
    {
        private VariableFloat moveSpeed;
        public BTMove(VariableFloat moveSpeed)
        {
            this.moveSpeed = moveSpeed;
        }

        public override BTNodeStatus Run()
        {
            moveSpeed.Value = 3;
            return BTNodeStatus.Success;
        }
    }
    public class BTAnimate : BTBaseNode
    {
        public BTAnimate() { }

        public override BTNodeStatus Run()
        {
            return BTNodeStatus.Success;
        }
    }

    public abstract class BTBaseNode
    {
        public virtual void OnEnter(){}
        public virtual void OnExit(){}
        public abstract BTNodeStatus Run();
        private bool isInitialized = false;
        public void BTNodeStatus OnUpdate(){
            if(!isInitialized){
                isInitialized = true;
                OnEnter();
            }
            var result = Run();
            if(result != BTNodeStatus.Running){
                OnExit();
                isInitialized = false;
            }
            return result;
        }
    }

    public class SomeAI : MonoBehaviour
    {

        [SerializeField] private VariableFloat moveSpeed;

        private BTBaseNode behaviourTree;
        private void Start()
        {
            moveSpeed = Instantiate(moveSpeed);


            behaviourTree = 
                new BTSequence(
                    new BTAnimate(),
                    new BTMove(moveSpeed),
                    new BTAnimate(),
                    new BTSequence(
                        new BTAnimate(),
                        new BTMove(moveSpeed),
                        new BTAnimate()
                    )
                );
        }
        
        public void Update(){
        
            behaviourTree?.OnUpdate();
        }

    }
}
