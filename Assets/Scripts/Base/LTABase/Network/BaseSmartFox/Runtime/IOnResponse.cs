using System.Collections;
using System.Collections.Generic;
using Sfs2X.Entities.Data;
using UnityEngine;
namespace LTA.SFS.Base
{
    public interface IOnResponse
    {
        void OnResponse(ISFSObject data);
    }
}
