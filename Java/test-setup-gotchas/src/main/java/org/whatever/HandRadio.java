package org.whatever;

public class HandRadio {
    private int frequency;
    private boolean toSecureModeTo;

    public void setFrequency(int frequency) {
        this.frequency = frequency;
    }

    public void turnOn() {

    }

    public void setSecureModeTo(boolean toSecureModeTo) {
        this.toSecureModeTo = toSecureModeTo;
    }

    public String receive(String encryptedContent) {
        return "lolek";
    }

    public void turnOff() {

    }

    public void send(String message) {

    }

    public int getFrequency() {
        return frequency;
    }
}
