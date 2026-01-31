# Selective Repeat Flow Control Simulator

## Table of Contents

- [Overview](#overview)  
- [Features](#features)  
- [Components](#components)  
- [Simulation Flow](#simulation-flow)  
- [Parameters](#parameters)  
- [How to Run](#how-to-run)  
- [Example Output](#example-output)  
- [Notes](#notes)

## Overview

This project simulates the **Selective Repeat (SR) flow control protocol** in a network environment using C#.  
It demonstrates:

- Packet transmission and acknowledgements  
- Handling packet loss and retransmissions  
- Sliding window flow control  
- Calculation of network performance metrics (average delay, retransmissions, total packets sent)  

## Features

- Configurable **window size**, **bandwidth**, **propagation delay**, **packet loss rate**  
- Tracks metrics: `TotalSent`, `TotalAcked`, `Retransmissions`, `Average Delay`  
- Simulates a network link between a sender and receiver  
- Simple and lightweight — no external libraries required  

## Components

### Packet

Represents a data packet:

- `SeqNum` → Sequence number  
- `SendTime` → Time sent  
- `Acked` → Whether the packet is acknowledged  

### Matrics

Tracks simulation metrics:

- `TotalSent`  
- `TotalAcked`  
- `Retransmissions`  
- `TotalDelay`  

### Receiver

Simulates a network receiver:

- Marks incoming packets as acknowledged  

### SimulationLink

Represents the network:

- `PacketSizeBytes` → Packet size  
- `WindowSize` → Sender window size  
- `BandwidthMbps` → Link bandwidth  
- `PropagationDelayMs` → Network delay  
- `LossRate` → Probability of packet loss  

### Sender

Implements Selective Repeat:

- Maintains a sliding window  
- Sends new packets while window permits  
- Handles ACKs and timeouts  
- Updates metrics  

## Simulation Flow

1. Sender initializes and starts transmitting packets.  
2. Packets travel through the SimulationLink (may be delayed or lost).  
3. Receiver marks successfully delivered packets as acknowledged.  
4. Sender checks for timeouts and retransmits lost packets.  
5. Sliding window advances as ACKs arrive.  
6. Metrics are recorded: Total Sent, Total Acked, Retransmissions, Average Delay.  

## Parameters

| Parameter | Description | Default Value |
|-----------|------------|---------------|
| PacketSizeBytes | Size of each packet | 1024 |
| WindowSize | Sender sliding window size | 8 |
| BandwidthMbps | Network bandwidth | 10 Mbps |
| PropagationDelayMs | Network delay | 50 ms |
| LossRate | Packet loss probability | 0.05 |
| TotalPackets | Total packets to send | 100 |
| TimeoutMs | Timeout for retransmission | 120 ms |

## How to Run

1. Open the project in **Visual Studio** or your preferred C# IDE.  
2. Build the solution.  
3. Run the executable (`Program.cs` contains the main entry point).  
4. The console prints:

- Packet transmissions and losses  
- Retransmissions due to timeouts  
- Final metrics including **average delay**  
