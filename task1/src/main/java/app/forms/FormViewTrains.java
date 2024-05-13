package app.forms;

import app.entity.Train;
import app.service.TrainService;
import app.util.HebirnateUtil;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.List;

public class FormViewTrains extends JFrame {
    private JPanel panelMain;
    private JButton buttonUpdateRequest;
    private JTable tableTrains;

    public FormViewTrains() {
        setTitle("View trains");
        setContentPane(panelMain);
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
        setSize(800, 800);
        setVisible(true);
        Object[] columns = {"Route", "Start", "Finish", "Total tickets", "Available tickers"};
        DefaultTableModel model = new DefaultTableModel(0, columns.length);
        tableTrains.setModel(model);
        model.addRow(columns);
        setTrains();

        buttonUpdateRequest.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                setTrains();
            }
        });
    }

    private void setTrains() {
        DefaultTableModel model = (DefaultTableModel) tableTrains.getModel();
        model.setRowCount(1);
        TrainService trainService = new TrainService(HebirnateUtil.getEntityManager());
        List<Train> trains = trainService.getAllTrains();
        trains.forEach(t -> setTrain(t));
    }

    private void setTrain(Train train) {
        DefaultTableModel model = (DefaultTableModel) tableTrains.getModel();
        Object[] obj = new Object[5];
        obj[0] = train.getStartCity().getName() + " -> " + train.getFinishCity().getName();
        obj[1] = train.getStartTime().toString();
        obj[2] = train.getFinishTime().toString();
        obj[3] = train.getTickets().size();
        obj[4] = train.getTickets().size() - train.getTickets().stream().filter(ticket -> ticket.getStatus()).count();
        model.addRow(obj);
    }
}
