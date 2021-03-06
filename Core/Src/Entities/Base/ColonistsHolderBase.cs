using System;
using Core.Entities.Interfaces;

namespace Core.Entities.Base
{
    public class ColonistsHolderBase : IColonistsHolder
    {
        public int MaxColonistsCount { get; }

        public bool IsActive => CurrentColonistsCount > 0;

        public int CurrentColonistsCount { get; private set; }

        protected ColonistsHolderBase(int maxColonistsCount)
        {
            MaxColonistsCount = maxColonistsCount;
        }

        public void Move(IColonistsHolder destination, int count = 1)
        {
            CurrentColonistsCount -= count;

            if (CurrentColonistsCount <= 0)
            {
                throw new InvalidOperationException("Too less colonists");
            }

            destination.ReceiveColonist(count);
        }

        public void ReceiveColonist(int count = 1)
        {
            CurrentColonistsCount += count;

            if (CurrentColonistsCount > MaxColonistsCount)
            {
                throw new InvalidOperationException("Too much colonists");
            }
        }
    }
}