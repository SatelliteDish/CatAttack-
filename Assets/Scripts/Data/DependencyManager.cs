using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyManager : MonoBehaviour
{
[SerializeField]ManagersRepo managersRepo;
[SerializeField]CosmeticsRepo cosmeticsRepo;
[SerializeField]WorldGenerationRepo worldGenerationRepo;
[SerializeField]UIRepo uIRepo;
private void Awake() {
    SetSingletons();
}
void SetSingletons(){
    managersRepo.SetAudioManager(FindObjectOfType<AudioManager>());
}
public void SetTestHelper(TestHelper testHelper){
    managersRepo.SetTestHelper(FindObjectOfType<DependencyManager>().GetManagersRepo().GetTestHelper());
    cosmeticsRepo.SetTestHelper(FindObjectOfType<DependencyManager>().GetManagersRepo().GetTestHelper());
    worldGenerationRepo.SetTestHelper(FindObjectOfType<DependencyManager>().GetManagersRepo().GetTestHelper());
    uIRepo.SetTestHelper(FindObjectOfType<DependencyManager>().GetManagersRepo().GetTestHelper());
}
public ManagersRepo GetManagersRepo(){
    return managersRepo;
}
public CosmeticsRepo GetCosmeticsRepo(){
    return cosmeticsRepo;
}
public WorldGenerationRepo GetWorldGenerationRepo(){
    return worldGenerationRepo;
}
public UIRepo GetUIRepo(){
    return uIRepo;
}
}
