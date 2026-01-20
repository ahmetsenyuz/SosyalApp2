# SosyalApp2 Database Design

## Overview
This document outlines the core database schema for the SosyalApp2 social task tracking application, defining entities and relationships necessary for user profiles, tasks, friendships, and scoring system to support the MVP requirements.

## Entities and Relationships

### User Table
- **userId** (Primary Key, UUID)
- **username** (Unique, String, 50 characters)
- **email** (Unique, String, 255 characters)
- **passwordHash** (String, 255 characters)
- **profilePictureUrl** (String, 500 characters)
- **firstName** (String, 100 characters)
- **lastName** (String, 100 characters)
- **bio** (Text)
- **dateOfBirth** (Date)
- **location** (String, 200 characters)
- **createdAt** (DateTime)
- **updatedAt** (DateTime)
- **isActive** (Boolean)

### Task Table
- **taskId** (Primary Key, UUID)
- **userId** (Foreign Key to User.userId)
- **title** (String, 255 characters)
- **description** (Text)
- **difficultyLevel** (Enum: Easy, Medium, Hard)
- **status** (Enum: Pending, In Progress, Completed, Failed)
- **points** (Integer)
- **dueDate** (DateTime)
- **createdAt** (DateTime)
- **updatedAt** (DateTime)
- **isPublic** (Boolean)

### Friendship Table
- **friendshipId** (Primary Key, UUID)
- **userId** (Foreign Key to User.userId)
- **friendId** (Foreign Key to User.userId)
- **status** (Enum: Pending, Accepted, Rejected, Blocked)
- **createdAt** (DateTime)
- **updatedAt** (DateTime)

### Scoring Table
- **scoreId** (Primary Key, UUID)
- **userId** (Foreign Key to User.userId)
- **taskId** (Foreign Key to Task.taskId)
- **pointsEarned** (Integer)
- **timestamp** (DateTime)
- **reason** (String, 200 characters)

### Photo Evidence Table
- **evidenceId** (Primary Key, UUID)
- **taskId** (Foreign Key to Task.taskId)
- **userId** (Foreign Key to User.userId)
- **imageUrl** (String, 500 characters)
- **uploadedAt** (DateTime)

## Relationships
1. One User can have many Tasks
2. One User can have many Friendships (as user and friend)
3. One User can have many Scores
4. One Task can have many Photo Evidence records
5. One Friendship connects two Users
6. One Score is associated with one User and one Task

## Data Integrity Constraints
- All foreign key relationships are enforced
- Unique constraints on username and email
- Not null constraints on required fields
- Check constraints for valid enum values
- Timestamps are automatically managed