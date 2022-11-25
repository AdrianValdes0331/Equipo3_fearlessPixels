using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class MultiplayerController : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public int id;


    [Header("Components")]
    public Rigidbody2D rig;
    public Player photonPlayer;
    [SerializeField] TextMeshProUGUI nameText;

    // Start is called before the first frame update
    void Start()
    {
    }

    /// <summary>
    /// Inicializar la informacion del player actual
    /// </summary>
    /// <param name="player"> Data de player</param>
    [PunRPC]
    public void Init(Player player)
    {
        photonPlayer = player;// Asignar el player actual
        id = player.ActorNumber;//Guardar el id del player
        SpawnMultiplayer.instance.players[id - 1] = this;// Asiganarlo a las lista de player dentro del game controller
        nameText.text = player.NickName;

        if (!photonView.IsMine) // Verificar si el movimiento es del usuario actual
        {
            rig.isKinematic = true;
        }
    }
}