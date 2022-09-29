public class PlayerStates {
    bool isBoosting = false;
    bool isInAir = false;
    bool isAlive = true;
    bool canMove = true;
    bool isRespawning = false;
    bool isClothed = false;
    bool hasRespawned = false;
    bool isInTutorial = false;

    public bool IsBoosting() {
        return isBoosting;
    }
    
    public void SetIsBoosting(bool state) {
        isBoosting = state;
    }

    public bool IsInAir() {
        return isInAir;
    }

    public void SetIsInAir(bool state) {
        isInAir = state;
    }

    public bool IsAlive() {
        return isAlive;
    }

    public void SetIsAlive (bool state) {
        isAlive = state;
    }

    public bool CanMove() {
        return canMove;
    }

    public void SetCanMove (bool state) {
        canMove = state;
    }
    public bool IsRespawning() {
        return isRespawning;
    }
    public void SetIsRespawning (bool state) {
        isRespawning = state;
    }

    public bool IsClothed() {
        return isClothed;
    }

    public void SetIsClothed (bool state) {
        isClothed = state;
    }
    public bool HasRespawned() {
        return hasRespawned;
    }

    public void SetHasRespawned (bool state) {
        hasRespawned = state;
    }
    public bool IsInTutorial() {
        return isInTutorial;
    }
    
    public void SetIsInTutorial (bool state) {
        isInTutorial = state;
    }
}