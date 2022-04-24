package handlers

import (
	"encoding/json"
	"exam_service/pkg/domain/models"
	"exam_service/pkg/errs"
	"exam_service/pkg/service"
	"github.com/gin-gonic/gin"
	"log"
	"net/http"
)

type QuestionHandlers struct {
	service service.QuestionService
}

func (questionHandler QuestionHandlers) Add(c *gin.Context) {
	c.Writer.Header().Add("Content-Type", "application/json")
	var newQuestion models.Question
	_ = json.NewDecoder(c.Request.Body).Decode(&newQuestion)
	err := validate.Struct(newQuestion)
	if err != nil {
		log.Println(errs.ErrRequiredFieldsAreMissed.Error())
		c.Writer.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(c.Writer).Encode(errs.NewResponse(errs.ErrRequiredFieldsAreMissed.Error(), http.StatusBadRequest))
		return
	}
	questionAdded, err := questionHandler.service.Add(c.Param("examId"), newQuestion)

	//handling errors
	if err != nil && err.Error() == errs.ErrDb.Error() {
		log.Println(errs.ErrDb.Error())
		c.Writer.WriteHeader(http.StatusInternalServerError)
		json.NewEncoder(c.Writer).Encode(errs.NewResponse(errs.ErrDb.Error(), http.StatusInternalServerError))
		return
	} else if err != nil && err.Error() == errs.ErrDuplicateExam.Error() {
		log.Println(errs.ErrDuplicateExam.Error())
		c.Writer.WriteHeader(http.StatusBadRequest)
		json.NewEncoder(c.Writer).Encode(errs.NewResponse(errs.ErrDuplicateExam.Error(), http.StatusBadRequest))
		return
	}

	//sending the response
	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	c.Writer.WriteHeader(http.StatusOK)
	json.NewEncoder(c.Writer).Encode(questionAdded)
}
