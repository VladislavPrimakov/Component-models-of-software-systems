package app.service;

import java.util.ArrayList;
import java.util.List;

public class Result <T> {
    private List<String> errors;
    private T data;

    public Result() {
        errors = new ArrayList<String>();
        data = null;
    }

    public Result(List<String> errors, T data) {
        this.errors = errors;
        this.data = data;
    }

    public void addError(String error) {
        errors.add(error);
    }

    public void setData(T data) {
        this.data = data;
    }

    public List<String> getErrors() {
        return errors;
    }

    public T getData() {
        return data;
    }

    public boolean hasErrors() {
        return errors != null && !errors.isEmpty();
    }

    public boolean hasData() {
        return data != null;
    }
}
