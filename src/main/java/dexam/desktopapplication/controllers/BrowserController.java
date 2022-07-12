package dexam.desktopapplication.controllers;

import dexam.desktopapplication.Main;
import dexam.desktopapplication.anticheat.AntiCheat;
import dexam.desktopapplication.messaging.RabbitMq;
import dexam.desktopapplication.utility.BrowsersList;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;

import java.io.IOException;
import java.net.URL;
import java.util.Map;
import java.util.ResourceBundle;
import java.util.concurrent.TimeUnit;

public class BrowserController implements Initializable {
    @FXML
    public Button startButton;
    @FXML
    public ComboBox comboBox;
    @FXML
    public Label errorText;

    private final Map<String, String> browsers = BrowsersList.getBrowsersList();
    private final ObservableList<String> observableBrowsersList = FXCollections.observableArrayList(browsers.keySet().toArray(new String[0]));

    public void startAntiCheat() throws IOException, InterruptedException {
        if (comboBox.getValue() == null) {
            errorText.setText("Please select a browser");
            return;
        } else {
            errorText.setText("Please wait till we get everything ready");
            String selectedBrowser = comboBox.getValue().toString();
            Main.usedBrowser = browsers.get(selectedBrowser);
            System.out.println(Main.usedBrowser);
        }

        boolean messageSent = RabbitMq.send("open-" + Main.userId);

        if(!messageSent) {
            errorText.setText("Error happened. Please try again");
            return;
        }
        TimeUnit.SECONDS.sleep(2);
        AntiCheat.initiate(Main.usedBrowser);
        Main.changeScene("stopApp.fxml");
    }
    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        comboBox.setItems(observableBrowsersList);
    }
}
