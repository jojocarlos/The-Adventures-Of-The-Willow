using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathCountInfo : MonoBehaviour, IDataPersistence
{
	//Count deads
	public int deathCount = 0;
	public TextMeshProUGUI death_text;
	
    // Start is called before the first frame update
    void Start()
    {
        DataPersistenceManager.instance.LoadGame(); 
		death_text.text = deathCount.ToString();
    }

    // Update is called once per frame
    public void DeadOneMore()
    {
		DataPersistenceManager.instance.SaveGame();
		death_text.text = deathCount.ToString();
        deathCount++;
    }
	
	public void LoadData(GameData data)
    {
        this.deathCount = data.deathCount;
    }

    public void SaveData(GameData data)
    {
        data.deathCount = this.deathCount;
    }
}
