using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyManager<T> : MonoBehaviour
{
[SerializeField]ManagersRepo<T> managersRepo;
[SerializeField]CosmeticsRepo cosmeticsRepo;
[SerializeField]WorldGenerationRepo<T> worldGenerationRepo;
[SerializeField]UIRepo uIRepo;
private void Awake() {
    SetSingletons();
}
void SetSingletons(){
    managersRepo.SetAudioManager(FindObjectOfType<AudioManager>());
}
public void SetTestHelper(TestHelper testHelper){
    managersRepo.SetTestHelper(FindObjectOfType<DependencyManager<T>>().GetManagersRepo().GetTestHelper());
    cosmeticsRepo.SetTestHelper(FindObjectOfType<DependencyManager<T>>().GetManagersRepo().GetTestHelper());
    worldGenerationRepo.SetTestHelper(FindObjectOfType<DependencyManager<T>>().GetManagersRepo().GetTestHelper());
    uIRepo.SetTestHelper(FindObjectOfType<DependencyManager<T>>().GetManagersRepo().GetTestHelper());
}
public ManagersRepo<T> GetManagersRepo(){
    return managersRepo;
}
public CosmeticsRepo GetCosmeticsRepo(){
    return cosmeticsRepo;
}
public WorldGenerationRepo<T> GetWorldGenerationRepo(){
    return worldGenerationRepo;
}
public UIRepo GetUIRepo(){
    return uIRepo;
}
}
