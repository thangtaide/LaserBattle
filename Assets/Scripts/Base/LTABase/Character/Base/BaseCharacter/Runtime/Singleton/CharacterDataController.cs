using LTA.DesignPattern;
using System;

public class CharacterDataController : Singleton<CharacterDataController>
{
    public CharactersVO charactersVO;

    public AnimationVO animationVO;
    public void LoadDataLocal()
    {
        charactersVO = new CharactersVO();
        animationVO = new AnimationVO();
    }
}
