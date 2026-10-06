using UnityEngine;

namespace Midterm
{
    public interface IPickable
    {

        public void OnPicked(Transform attachTransform);
        public void OnDropped();


    }
}
