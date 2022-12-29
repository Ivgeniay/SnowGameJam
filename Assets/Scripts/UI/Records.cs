using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Records : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI firstTextMeshPro;
        [SerializeField] private TextMeshProUGUI secondTextMeshPro;
        [SerializeField] private TextMeshProUGUI thirdTextMeshPro;
        [SerializeField] private TextMeshProUGUI fourthTextMeshPro;
        [SerializeField] private TextMeshProUGUI fifthTextMeshPro;

        private Dictionary<string, int> _records = new Dictionary<string, int>();
        private void OnEnable() {
            _records = Game.Game.Manager.PlayerRepository.GetRecords();
            firstTextMeshPro.text = _records[Game.Game.Manager.PlayerRepository.FIRST].ToString();
            secondTextMeshPro.text = _records[Game.Game.Manager.PlayerRepository.SECOND].ToString();
            thirdTextMeshPro.text = _records[Game.Game.Manager.PlayerRepository.THIRD].ToString();
            fourthTextMeshPro.text = _records[Game.Game.Manager.PlayerRepository.FOURTH].ToString();
            fifthTextMeshPro.text = _records[Game.Game.Manager.PlayerRepository.FIFTH].ToString();
        }
    }
}
